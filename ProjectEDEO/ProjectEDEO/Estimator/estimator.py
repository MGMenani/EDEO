import os
import sys

import warnings

import numpy as np

from PIL import Image

from keras import backend as k
from keras.models import load_model

warnings.simplefilter(action='ignore', category=FutureWarning)
np.set_printoptions(threshold=np.nan)

running_dir = os.path.dirname(os.path.abspath(__file__))


def load_image(file):
    # Receive a file path in string
    # Open and converts the image
    # Returns the raw array image
    image = Image.open(file)
    image = image.convert('L')
    image = image.resize((255, 255))
    raw_image = np.array(list(image.getdata()))
    image.close()

    return raw_image


def evaluate(model, data):
    # Estimate the bone age
    # Receive a Keras model and a raw image
    # Returns the estimation
    data = format_x(data, (255, 255))
    return model.predict(data, verbose=True)


def format_x(x, image_shape):
    # Reshapes somehow the image in order to make it processable by the model
    print(x.shape)
    unscaled = x.reshape(1, image_shape[0], image_shape[1]).astype('float16')
    stacked = np.stack([unscaled, unscaled, unscaled], axis=3)
    stacked /= 255
    return stacked


def main(image, gender):
    # Loads the image
    image = load_image(image)

    # Load the female or male model
    # This model are not the weight only. This are the FULL Keras model
    if gender == "female":
        model = load_model(running_dir + ".\\female_model.h5")
    elif gender == "male":
        model = load_model(running_dir + ".\\male_model.h5")

    # Evaluates the image
    result = evaluate(model, image)
    k.clear_session()

    return "%.2f" % result


file_name, gender_param = sys.argv[1], sys.argv[2]
print(main(file_name, gender_param))
