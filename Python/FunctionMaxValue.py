#遗传算法求函数极值

import numpy as np
from matplotlib import pyplot as plt

def initpop(popsize,binarylength):
    pop = np.random.randint(0,2,(popsize,binarylength)) #生成popsize×binarylength的二维0、1序列
    return pop

def bintodec(ypop):
    pop=ypop.copy()
    [row,col] = pop.shape
    for i in range(col):
        pop[:,i]=2**(col-1-i)*pop[:,i]
    pop = np.sum(pop,axis=1)
    num=[]
    num=pop*10/1023
    return num

#计算种群适应度
def cal_objval(pop):
    x = bintodec(pop)
    objval = 10*np.sin(5*x)+7*abs(x-5)+10
    return objval

def selection(pop,fitval,popsize):
    idx = np.random.choice(np.arange(popsize),size=popsize,replace=True,p=fitval/fitval.sum())
    return pop[idx]

def crossover(pop,pc):
    [px,py] = pop.shape
    newpop = np.ones((px,py))
    for i in range(0,px,2):
        if np.random.rand()<pc:
            cpoint = int(np.random.rand()*py*10//10)
            newpop[i,0:cpoint]=pop[i,0:cpoint]
            newpop[i,cpoint:py]=pop[i+1,cpoint:py]
            newpop[i+1,0:cpoint]=pop[i+1,0:cpoint]
            newpop[i+1,cpoint:py]=pop[i,cpoint:py]
#             newpop[i+1,:]=[pop[i+1,0:cpoint],pop[i,cpoint:py]]
        else:
            newpop[i,:]=pop[i,:]
            newpop[i+1,:]=pop[i+1,:]
    return newpop

    
def mutation(pop,pm):
    [px,py] = pop.shape
    newpop = np.ones((px,py))
    for i in range(px):
        if(np.random.rand()<pm):
            mpoint = int(np.random.rand()*py*10//10)
            if mpoint<=0:
                mpoint=1
            newpop[i,:]=pop[i,:]
            if newpop[i,mpoint]==0:
                newpop[i,mpoint]=1
            else:
                newpop[i,mpoint]=0
        else:
            newpop[i,:]=pop[i,:]
    return newpop

def best(pop,fitvalue):
    [px,py]=pop.shape
    bestindividual = pop[0,:]
    bestfit = fitvalue[0]
    for i in range(1,px):
        if fitvalue[i]>bestfit:
            bestindividual = pop[i,:]
            bestfit = fitvalue[i]
    return bestindividual,bestfit
            

if __name__=="__main__":
    popsize = 100 #种群规模
    binarylength = 10 #二进制编码长度（DNA）
    pc = 0.6 #交叉概率
    pm = 0.001  #变异概率
    pop = initpop(popsize,binarylength) #初始化种群

    #进行计算
    for i in range(100):
        #计算当前种群适应度
        objval = cal_objval(pop)
        fitval = objval
        
        #选择操作
        newpop = selection(pop,fitval,popsize)
        #交叉操作
        newpop = crossover(newpop,pc);
        #变异操作
        newpop = mutation(newpop,pm);
        #更新种群
        pop = newpop;

        
        #寻找最优解并绘图
        [bestindividual,bestfit]=best(pop,fitval)
        
        x1 = bintodec(newpop)
        y1 = cal_objval(newpop)
        x = np.arange(0,10,0.1)
        y = 10*np.sin(5*x)+7*abs(x-5)+10
        if i%10==0:
            plt.figure()
            plt.rcParams["font.sans-serif"] = ["SimHei"]   #解决中文乱码问题
            plt.rcParams["axes.unicode_minus"] = False   #使一些符号正常显示
            plt.plot(x,y)
            plt.plot(x1,y1,'*')
            plt.title('迭代次数为:%d'%i)
    [n]=bestindividual.shape
    x2=0
    for i in range(n):
        x2+=2**(n-1-i)*bestindividual[i]
    print("The best X is ",x2*10/1023)
    print("The best Y is ",bestfit)
        
