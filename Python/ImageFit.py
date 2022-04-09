#遗传算法实现图像拟合
import numpy
from PIL import Image, ImageDraw
import cv2
import random
import os
import datetime

from sewar.full_ref import mse, rmse, psnr, uqi, ssim, ergas, scc, rase, sam, msssim, vifp

img_path = r"D:\\Code\\CSharp\\RevitPlugIns\\RvtPlugIn\\Python\\Image"  # 图片文件夹
img_name = "11.png"  # 图片文件名
img_obj = cv2.imread(os.path.join(img_path, img_name))
max_sim = float("inf")  # 初始化相似度描述量，根据当前的相似度算法，相似度越高此值越小（根据不同的相似度算法含义不同）

index = 0  # 记录当前到第几代了

vertices = 3
polygons = list()  # 当前所有的个体
pop_num = 16  # 每代的个体数量
polygon_num = 60  # 多边形数量，多了会影响性能
img_width = 100  # 图片宽
img_height = 100  # 图片高


def main():
    init()  # 生成第一代
    for _ in range(20000):
        select()  # 选择
        cross()  # 生成后一代


def init():
    """初始化"""
    global index
    for _ in range(pop_num):
        triangles = [get_random_coord() for _ in range(polygon_num)]
        colors = [get_random_rgb() for _ in range(polygon_num)]
        polygons.append(PolygonImage(index, triangles, colors))
        index += 1


def select():
    """选择，淘汰掉一部分不想要的后代"""
    polygons.sort(key=lambda x: x.sim)
    polygons[5:] = []


def cross():
    """产生一代后代"""
    while len(polygons) < pop_num:
        poly = gen()
        polygons.append(poly)


def gen():
    """生成下一代，优胜劣汰和变异"""
    global index
    temp_coords = list()
    temp_colors = list()

    for i in range(polygon_num):
        if random.random() < 0.95:
            '''选择基因的来源父母，95%几率从最优的祖先中随机'''
            poly_a = random.choice(polygons[:1])
            poly_b = random.choice(polygons[1:5])
        else:
            '''选择基因的来源父母，5%从所有的祖先中随机'''
            poly_a = random.choice(polygons)
            poly_b = random.choice(polygons)
        temp = random.random()
        if temp < 1 / polygon_num:
            '''设定一定几率坐标变异'''
            rnd_temp_coord = poly_a.coord_list[i][:]
            rnd_temp_coord[random.randint(0, vertices - 1)] = random.randint(0, img_width), random.randint(0, img_height)
            temp_coords.append(rnd_temp_coord)
        elif temp < 0.5:
            '''随机继承父母中的一个基因'''
            temp_coords.append(poly_b.coord_list[i])
        else:
            temp_coords.append(poly_a.coord_list[i])
        temp = random.random()
        if temp < 1 / polygon_num:
            '''设定一定几率颜色变异'''
            rnd_temp_color = list(poly_a.rgba_list[i])
            rnd_temp_color[random.randint(0, 3)] = random.randint(0, 255)
            temp_colors.append(tuple(rnd_temp_color))
        elif temp < 0.5:
            temp_colors.append(poly_b.rgba_list[i])
        else:
            temp_colors.append(poly_a.rgba_list[i])
    tri_temp = PolygonImage(index, temp_coords, temp_colors)
    index += 1
    return tri_temp


class PolygonImage:
    def __init__(self, idx, coord_list, rgba_list):
        self.coord_list = coord_list  # 图形坐标数组
        self.rgba_list = rgba_list  # 颜色rgba数组
        self.index = idx
        self.image_cv2, self.image = self.draw_polygon()  # 画图
        self.sim = ergas(img_obj, self.image_cv2)  # 评估相似度
        global max_sim
        if self.sim < max_sim * 0.99:  # 只有相似描述量小于前一次的0.99才更新，否则产生的图片太多
            max_sim = self.sim
            self.image.save(os.path.join(gen_path, '{:0>8d}.png'.format(self.index)), 'png')
            print(f"第{self.index}代, 相似度描述量为：{self.sim}")

    def draw_polygon(self):
        """根据coord_list和rgb_list画出图形"""
        width = img_width
        height = img_height
        image = Image.new('RGBA', (width, height))

        for i in range(len(self.rgba_list)):
            image_temp = Image.new('RGBA', (width, height))
            draw_temp = ImageDraw.Draw(image_temp)
            draw_temp.polygon(self.coord_list[i], fill=self.rgba_list[i])
            image = Image.alpha_composite(image, image_temp)  # 两张图alpha叠加
        image_cv2 = cv2.cvtColor(numpy.asarray(image), cv2.COLOR_RGB2BGR)
        return image_cv2, image


def get_random_coord():
    """随机产生一个多边形的一组坐标"""
    points = []
    for _ in range(vertices):
        points.append((random.randint(0, img_width), random.randint(0, img_height)))
    return points


def get_random_rgb():
    """随机产生一组RGBA颜色值"""
    return random.randint(0, 255), random.randint(0, 255), random.randint(0, 255), random.randint(0, 255)


if __name__ == '__main__':
    now = datetime.datetime.now()
    gen_path = f"{img_name.split('.')[0]}_{now.month}_{now.day}_{now.hour}_{now.minute}"
    while os.path.exists(gen_path):
        gen_path = gen_path + "_"
    os.mkdir(gen_path)
    with open(os.path.join(gen_path, "_info.txt"), mode="w", encoding="utf8") as fp:
        fp.write(f"""vertices = {vertices} pop_num = {pop_num} polygon_num = {polygon_num} img_width = {img_width} img_height = {img_height}""")
    main()