from ast import Pass
import numpy as np
from matplotlib import pyplot as plt

#求x**2 + 0.3*y**2 + 10 在-10~10范围内的极值

popsize = 100 #种群规模
binarylength = 10 #二进制编码长度（DNA）前5位为X，后5位为Y
pc = 0.6 #交叉概率
pm = 0.001  #变异概率


def f(x,y):
    return x**2 + 0.3*y**2 + 10

def initpop():
    pop = np.random.randint(0,2,(100,10))
    return pop

def bin2dec(row):
    dec = 0
    for i in range(row.shape[0]):
        dec += row[row.shape[0]-i-1]*(2**i)
    return dec


def plotpop(pop,coors):
    pxs = []
    pys = []
    for i in range(pop.shape[0]):
        pxs.append(bin2dec(pop[i][:5]))
        pys.append(bin2dec(pop[i][5:]))
    plt.scatter(coors[:,:,0],coors[:,:,1],s=1,c=255-coors[:,:,2])
    plt.scatter(pxs,pys,s=5,c="red")
    # ps = np.random.uniform(-10,10,(200,2))
    # plt.scatter(ps[:,0],ps[:,1],c="red")
    plt.axis("equal")

#计算种群适应度(返回一个向量，每个值代表单个个体的适应度)
def cal_fitval(population):
    fitval = np.empty(population.shape[0])
    for i in range(population.shape[0]):
        x = bin2dec(population[i,:5])
        y = bin2dec(population[i,5:])
        fitval[i] = 8 / f(x,y)
    return fitval

def cal_popval(population):
    fitval = np.empty(population.shape[0])
    for i in range(population.shape[0]):
        x = bin2dec(population[i,:5])
        y = bin2dec(population[i,5:])
        fitval[i] = f(x,y)
    return fitval.sum() / 100

#选择，基于概率的选择
def select(pop):
    fitval = cal_fitval(pop)
    ps = fitval / fitval.sum()
    idx = np.random.choice(np.arange(0,pop.shape[0]),size=pop.shape[0],replace=True,p=ps)
    return pop[idx]

#交叉(是否尽可能让优秀的个体才进行交配)
def cross(pop):
    newpop = np.empty(pop.shape)
    for i in range(0,100,2):
        ix = np.random.randint(0,5)
        iy = np.random.randint(5,10)
        p = np.random.rand()
        if p < pc:
            newpop[i,0:ix] = pop[i,0:ix]
            newpop[i,ix:5] = pop[i+1,ix:5]
            newpop[i,5:iy] = pop[i,5:iy]
            newpop[i,iy:10] = pop[i+1,iy:10]

            newpop[i+1,0:ix] = pop[i+1,0:ix]
            newpop[i+1,ix:5] = pop[i,ix:5]
            newpop[i+1,5:iy] = pop[i+1,5:iy]
            newpop[i+1,iy:10] = pop[i,iy:10]
        else:
            newpop[i,:] = pop[i,:]
            newpop[i+1,:] = pop[i+1,:]

    return newpop

#变异
def mutation(pop):
    newpop = pop.copy()
    for i in range(0,100,2):
        ix = np.random.randint(0,5)
        iy = np.random.randint(5,10)
        p = np.random.rand()
        if p < pm:
            newpop[i,ix] = 0 if pop[i,ix] == 1 else 0
            newpop[i,iy] = 0 if pop[i,iy] == 1 else 0
    return newpop


if __name__ == "__main__":
    plt.figure()
    #用于画图的坐标点
    xs = np.linspace(-10,10,200)
    ys = np.linspace(-10,10,200)
    points = np.empty((200,200,3))

    for i in range(xs.shape[0]):
        for j in range(ys.shape[0]):
            z = f(xs[i],ys[j])
            points[i,j] = (xs[i],ys[j],z)
    
    history_pop = []
    pop = initpop()
    history_pop.append(pop)

    for i in range(50):
        pop = select(pop)
        pop = cross(pop)
        pop = mutation(pop)
        history_pop.append(pop)
        print(cal_popval(pop))

    


    # for i in range(20):
    #     plt.subplot(4,5,i+1)
    #     plotpop(pop,points)
    # plt.show()

    for i in range(20):
        plt.subplot(4,5,i+1)
        plotpop(history_pop[i],points)
    plt.show()