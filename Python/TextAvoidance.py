#遗传算法解决问题避让

import numpy as np
from matplotlib import pyplot as plt

popsize = 100
xrange = [0,1]
yrange = [0,1]
pointsize = 200
pc = 0.6 #交叉概率
pm = 0.1 #变异概率


#初始化种群
def init():
    pops = np.empty((popsize,pointsize*2))

    # for i in range(0,20):
    #     j = 0
    #     while True:
    #         p = np.random.uniform(0,1,(1,2))
    #         d = ((p[0,0]-0.5)**2 + (p[0,1]-0.5)**2)**0.5
    #         if(d > 0.5):
    #             pops[i,j] = p[0,0]
    #             pops[i,j+200] = p[0,1]
    #             j += 1
    #         if(j >= 200):
    #             break

    for i in range(0,100):
        pops[i,:] = np.random.uniform(0,1,(1,400))
    return pops

#计算种群适应度
def cal_objval(ps):
    outn = 0
    cent = [0.5,0.5]
    for i in range(0,200):
        d = ((ps[i]-cent[0])**2 + (ps[i+200]-cent[1])**2)**0.5
        if(d >= 0.5):
            outn +=1
    return outn

#种群选择
def select(ps):
    fitvals = np.empty(100)
    for i in range(100):
        val = cal_objval(ps[i,:])
        fitvals[i] = val**50
    idx = np.random.choice(np.arange(100),size=100,replace=True,p=fitvals/fitvals.sum())
    return ps[idx]

#交叉
def cross(ps):
    newps = ps.copy()
    for i in range(0,50,2):
        if np.random.rand() < pc:
            a = ps[i,:]
            b = ps[i+1,:]
            j = np.random.randint(0,200)
            k = np.random.randint(200,400)

            a1 = a[0:j]
            a2 = b[j:200]
            a3 = a[200:k]
            a4 = b[k:]
            newa = np.hstack((a1,a2,a3,a4))

            b1 = b[0:j]
            b2 = a[j:200]
            b3 = b[200:k]
            b4 = a[k:]
            newb = np.hstack((b1,b2,b3,b4))
            newps[i,:] = newa
            newps[i+1,:] = newb
    return newps

#变异
def mutation(ps):
    newps = ps.copy()
    for i in range(100):
        if np.random.rand() < pm:
            mp1 = np.random.randint(0,200,5)
            mp2 = np.random.randint(200,400,5)
            newps[i,mp1] = np.random.uniform(0,1,5)
            newps[i,mp2] = np.random.uniform(0,1,5)
    return newps

nowpops = init()

data = []
for i in range(800):
    newpops = select(nowpops)
    newpops = cross(newpops)
    newpops = mutation(newpops)
    nowpops = newpops
    #打印所有种群的适应度值平均值
    sumval = 0.0
    for j in range(100):
        val = cal_objval(nowpops[j,:])
        sumval += val
    data.append(sumval/100)
    print("{0} -> {1}".format(i,sumval/100))

plt.figure()
plt.plot([i for i in range(len(data))],data)
plt.axis("equal")

plt.figure()
plt.scatter(nowpops[0,:200],nowpops[0,200:],s=2)
plt.axis("equal")

plt.show()