import numpy as np
import matplotlib.pyplot as plt
import matplotlib.image as mpimg
import os
from PIL import Image as PImage
from scipy import misc, signal
from skimage import filters, color


def applyFilter(full_image_path):
    image_path, image_name = os.path.split(full_image_path)  #Divide path and image name
    new_image_path = image_path +'\\Filter.png'
    scharr = [[0, 1, 0],[1, -4, 1],[0, 1, 0]]
    gaussian = [[1, 1, 1],[1, 1, 1],[1, 1, 1]]
    USM = 1/16*np.array([[-1, -2, -1],[-2, 12, -2],[-1, -2, -1]])


    image=color.rgb2gray(mpimg.imread(full_image_path))
    gauss_image = filters.gaussian(image, sigma = 5)
    laplace_image = filters.laplace(gauss_image, ksize = 11)
    B1 = -laplace_image
    B2 = -(image-gauss_image)
    USM = image+0.5*B2

    misc.imsave(new_image_path, USM)

    '''
    fig, axes = plt.subplots(nrows=1, ncols=3,
                             sharex=True, sharey=True,
                             figsize=(20, 20))

    
    ax = axes.ravel()

    ax[0].imshow(image, cmap=plt.cm.gray)
    ax[0].set_title('Original image')
    ax[1].imshow(B2, cmap=plt.cm.gray)
    ax[1].set_title('LoG')
    ax[2].imshow(USM, cmap=plt.cm.gray)
    ax[2].set_title('Unsharp Mask')
    
    for a in ax:
        a.axis('off')
    fig.tight_layout()
    plt.show()
    '''
    #print(USM)
    #plt.imshow(USM, cmap=plt.cm.gray)
    #plt.show()
    
    return(new_image_path)

#full_image_path = r'C:\Users\migra\Desktop\Proyecto\EDEO\ProjectEDEO\ProjectEDEO\images\portfolio\4538.png'
image_name, image_path = sys.argv[1], sys.argv[2]
print(applyFilter(full_image_path))
