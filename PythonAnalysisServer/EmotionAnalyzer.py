import socket
from PIL import Image,ImageFile
import io
import numpy as np
import cv2
import tensorflow as tf
from datetime import datetime


ImageFile.LOAD_TRUNCATED_IMAGES = True
serv = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
serv.bind(('localhost', 8080))
serv.listen(5)
#emotions = ["Neutral", "Calm", "Happy","Sad", "Angry", "Fearful", "Disgust", "Surprise" ]
emotions = ['angry','calm','disgust','fearful','happy','neutral','sad','surprise']

face_cascade = cv2.CascadeClassifier('haarcascade_frontalface_default.xml')
model = tf.keras.models.load_model('victory.h5')

while True:
    conn, addr = serv.accept()
    from_client = ''
    
    while True:
        data = conn.recv(663341)
        if not data: break

       
        if (data is not None):
            try:
                img = Image.open(io.BytesIO(data))
                open_cv_image = np.array(img) 
                # Convert RGB to BGR 
                open_cv_image = open_cv_image[:, :, ::-1].copy()
                gray = cv2.cvtColor(open_cv_image, cv2.COLOR_BGR2GRAY)
                faces = face_cascade.detectMultiScale(gray, 1.3, 5) 
                
                for (x,y,w,h) in faces: 
                    cv2.rectangle(open_cv_image,(x,y),(x+w,y+h),(255,255,0),2)  
                    roi_gray = gray[y:y+h, x:x+w] 
                    roi_color = open_cv_image[y:y+h, x:x+w] 
                    resized = cv2.resize(roi_color, (50,50), interpolation= cv2.INTER_AREA)
                    image = tf.expand_dims(resized, axis=0) 

                    classified_class = model.predict(image)
                    min_prob = np.amin(classified_class)
            
                    emotions_classes = np.around(np.divide(classified_class, min_prob), 2)
                    emotionAnalysis = ""
                    emotion = emotions[np.where(classified_class == np.amax(classified_class))[1][0]]
                    print(emotion)
                    emotionText = str(np.where(classified_class == np.amax(classified_class))[1][0])
                    arr = bytearray(emotion+"-"+ datetime.utcnow().strftime('%H:%M:%S.%f')[:-3], 'utf-8')
                    conn.sendall(arr)
            except:
                print('error')
               

        
        

    conn.close()
    print('client disconnected')
