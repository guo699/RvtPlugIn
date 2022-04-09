import numpy as np
from matplotlib import pyplot as plt

#让100个散点均匀分布在⚪外的正方形区域内
#每个点分为XY两个坐标，即每个个体为一个200长的向量，前100个X，后100为Y
#正方形区域为X = 0~1 Y = 0~1
#编码方式为浮点数编号

popsize = 200 #种群规模
pc = 0.6 #交叉概率
pm = 0.01  #变异概率

def initpop():
    pop = np.random.uniform(0,1,(popsize,200))
    return pop

#适应度函数，在圆外的点越多，适应度越高
def cal_fitval(points):
    n = 0
    d = 0
    for i in range(0,100):
        x = points[i]
        y = points[i+100]
        d = ((x-0.5)**2 + (y-0.5)**2)**0.5
        if d >= 0.5:
            n += 1
    return n

def select(pop):
    fitval = np.empty(pop.shape[0])
    for i in range(popsize):
        fitval[i] = cal_fitval(pop[i])

    ps = fitval / fitval.sum()
    idx = np.random.choice(np.arange(0,pop.shape[0]),size=pop.shape[0],replace=True,p=ps)
    return pop[idx]

def cross(pop):
    newpop = np.empty(pop.shape)
    for i in range(0,popsize,2):
        p = np.random.rand()
        if p < pc:
            ix = np.random.randint(0,100)
            iy = np.random.randint(0,100)
            newpop[i,0:ix] = pop[i,0:ix]
            newpop[i,ix:100] = pop[i+1,ix:100]
            newpop[i,100:iy] = pop[i,100:iy]
            newpop[i,iy:200] = pop[i+1,iy:200]

            newpop[i+1,0:ix] = pop[i+1,0:ix]
            newpop[i+1,ix:100] = pop[i,ix:100]
            newpop[i+1,100:iy] = pop[i+1,100:iy]
            newpop[i+1,iy:200] = pop[i,iy:200]

        else:
            newpop[i,:] = pop[i,:]
            newpop[i+1,:] = pop[i+1,:]

    return newpop

def mutation(pop):
    newpop = pop.copy()
    for i in range(0,popsize):
        p = np.random.rand()
        if p < pm:
            ix = np.random.randint(0,100)
            newpop[i,ix] = np.random.uniform(0,1)
        p = np.random.rand()
        if p < pm:
            iy = np.random.randint(100,200)
            newpop[i,iy] = np.random.uniform(0,1)
    return newpop

def meanFit(pop):
    val = 0
    for i in range(pop.shape[0]):
        val += cal_fitval(pop[i])
    return val / 200


if __name__ == "__main__":
    pop = initpop()

    for i in range(2000):
        pop = select(pop)
        pop = cross(pop)
        pop = mutation(pop)
        
        print(meanFit(pop))

    plt.figure()
    xs = pop[0,:100]
    ys = pop[0,100:]
    plt.scatter(xs,ys)
    plt.show()