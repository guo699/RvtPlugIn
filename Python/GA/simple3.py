from cmath import pi, sin
import numpy as np
from matplotlib import pyplot as plt

def f(x,y):
    return 21.5 + x*sin(4*pi*x) + y*sin(20*pi*y)


if __name__ == "__main__":
    xs = np.linspace(-3.0,12.1,200)
    ys = np.linspace(4.1,5.8,200)
    coors = np.empty((xs.shape[0],ys.shape[0],3))

    for i in range(xs.shape[0]):
        for j in range(ys.shape[0]):
            coors[i,j] = (xs[i],ys[j],f(xs[i],ys[j]))

    plt.figure()
    plt.scatter(coors[:,:,0],coors[:,:,1],c = coors[:,:,2])
    plt.show()