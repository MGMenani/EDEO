import numpy as np
import matplotlib.pyplot as plt
import matplotlib.image as mpimg
from PIL import Image as PImage
from scipy import misc, signal
from skimage import filters, color


image_path = r"4474.png"
scharr = [[0, 1, 0],[1, -4, 1],[0, 1, 0]]
gaussian = [[1, 1, 1],[1, 1, 1],[1, 1, 1]]
USM = 1/16*np.array([[-1, -2, -1],[-2, 12, -2],[-1, -2, -1]])


image=color.rgb2gray(mpimg.imread(image_path))
gauss_image = filters.gaussian(image, sigma = 11)
laplace_image = filters.laplace(gauss_image, ksize = 7)
B1 = -laplace_image
B2 = -(image-gauss_image)
USM = image+0.4*B2

fig, axes = plt.subplots(nrows=1, ncols=3,
                         sharex=True, sharey=True,
                         figsize=(20, 20))
ax = axes.ravel()

ax[0].imshow(image, cmap=plt.cm.gray)
ax[0].set_title('Original image')
ax[1].imshow(laplace_image, cmap=plt.cm.gray)
ax[1].set_title('LoG')
ax[2].imshow(USM+laplace_image, cmap=plt.cm.gray)
ax[2].set_title('Unsharp Mask')

for a in ax:
    a.axis('off')
fig.tight_layout()
plt.show()

