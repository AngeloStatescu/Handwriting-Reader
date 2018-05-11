import cv2
from operator import itemgetter
from sklearn import svm
from sklearn.ensemble import BaggingClassifier, RandomForestClassifier
import pickle

def overlap(arr1, arr2):
    if arr1[0]>arr2[0] and arr1[0]<arr2[0]+arr2[2] and arr1[1]>arr2[1] and arr1[1]<arr2[1]+arr2[3]:
        return True
    return False

def test():
    img = cv2.imread('test20.jpg')
    (h, w) = img.shape[:2]
    image_size = h*w
    mser = cv2.MSER_create()
    mser.setMaxArea(int(image_size/2))
    mser.setMinArea(40)

    gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
    bw = cv2.adaptiveThreshold(gray, 255.0, cv2.ADAPTIVE_THRESH_GAUSSIAN_C, cv2.THRESH_BINARY,111,7.58)
    regions, rects = mser.detectRegions(bw)
    cv2.imwrite('bw.jpg',bw)
    array=rects.tolist()
    #toremove=[]
    #for i in range(len(array)):
    #    if array[i][2]>110 or array[i][3]>110:
    #        toremove.append(array[i])
    #        
    #for item in toremove:
    #    array.remove(item)

    toremove=[]    
    array=sorted(array,key=itemgetter(1))
    for i in range(len(array)):
        for j in range(i):
            if overlap(array[i],array[j]):
                toremove.append(array[i])
                break

    for item in toremove:
        array.remove(item)

    hsum=0
    wsum=0
    wmultiplier=1.56
    for i in range(len(array)):
        hsum+=array[i][3]
        wsum+=array[i][2]
    avgh=int(hsum/len(array))+10
    avgw=int(wsum/len(array))+10

    print(avgw*wmultiplier)
    separator=0
    inith=array[0][1]
    for i in range(len(array)):
        if array[i][1]-avgh>inith:
            inith=array[i][1]
            array[separator:i]=sorted(array[separator:i],key=itemgetter(0))
            separator=i
    array[separator:len(array)]=sorted(array[separator:len(array)],key=itemgetter(0))



    unet_path='network.pkl'
    unet=open(unet_path,'rb')

    network=pickle.load(unet)

    result=''
    count=0

    for i in range(len(array)):
        x=array[i][0]
        y=array[i][1]
        w=array[i][2]
        h=array[i][3]
        print((x,y,w,h))
        if i > 0:
            if abs(x - array[i-1][0]) > avgw*wmultiplier:
                result+=' '
        
        src=bw[y:y+h, x:x+w]
        bottom=0
        top = int((128-src.shape[0])/2)  
        if src.shape[0]%2==1:
            bottom=top+1
        else:
            bottom=top

        right=0
        left = int((128-src.shape[1])/2)
        if src.shape[1]%2==1:
            right=left+1
        else:
            right=left
        if right<0 or left<0 or top<0 or bottom<0:
            ind_letter=cv2.resize(src,(128,128))
        else:
            ind_letter=cv2.copyMakeBorder(src,top,bottom,left,right,cv2.BORDER_CONSTANT,value=[255,255,255])
        img_path='img'+str(count)+'l.jpg'
        count+=1
        cv2.imwrite(img_path,ind_letter)
        result+=network.predict([cv2.resize(cv2.imread(img_path),(36,36)).ravel()])[0]

    f=open('result.txt','w')
    f.write(result)

test()
