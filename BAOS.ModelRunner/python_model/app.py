import pickle
import sys
import numpy as np
import Orange

model = pickle.load(open('model.pkcls','rb'))

def predict():
    features = [int(x) for x in sys.argv]
    final_features = [np.array(features)]
    prediction = model.predict(final_features)
    output = np.array(prediction[0])
    if output == 0:
       output == 'WAN'
    elif output == 1:
        output='LAN'
    elif output == 2:
        output='LPWAN'  