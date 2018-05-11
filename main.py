import numpy as np
from sklearn.tree import DecisionTreeClassifier
from sklearn.ensemble import BaggingClassifier, RandomForestClassifier
from skimage.io import imread
from sklearn.svm import SVC
from sklearn.multiclass import OneVsRestClassifier
import cv2
import os
import pickle
#0123456789aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ
rootdir = './dataset2'
reference='0123456789aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ'
alph=list(reference)
train=[]
labels=[]
first=True
alph_ind=-1
for subdir, dirs, files in os.walk(rootdir):
    if first==True:
        first=False
    else:
        alph_ind+=1
        img_count=0
        for file in files:
            img_count+=1
            train.append(cv2.resize(cv2.imread(os.path.join(subdir, file)),(36,36)).ravel())
        print(alph_ind)
        labels+=[alph[alph_ind]]*img_count
        print(img_count)
print(labels)
n_estimators=10
clf = OneVsRestClassifier(SVC(kernel='poly', probability=True, class_weight='balanced', degree=5))
clf.fit(train,labels)

net_path='network.pkl'
net=open(net_path,'wb')
pickle.dump(clf,net)
net.close()

unet_path='network.pkl'
unet=open(unet_path,'rb')

network=pickle.load(unet)
print(network.predict([cv2.resize(cv2.imread('tst.png'),(36,36)).ravel()]))
