
import cv2
from flask import Flask, render_template, Response

app = Flask(__name__)

frame_processed = 0
score_thresh = 0.2

def gen():
    """Video streaming generator function."""
    while True:
        # Constants for the crop size
        xMin = 0
        yMin = 0
        xMax = 1180
        yMax = 1080

        # Open cam, decode image, show in window
        cap = cv2.VideoCapture(1) # use 1 or 2 or ... for other camera
        
        cap.set(cv2.CAP_PROP_FRAME_WIDTH, 1080)
        cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 1080)
        cap.set(cv2.CAP_PROP_FPS, 30)

        #cv2.namedWindow("Original")
        #cv2.namedWindow("Cropped")
        key = -1
        while(key < 0):
            success, cropImg = cap.read()

            #cropImg = img[yMin:yMax,xMin:xMax] # this is all there is to cropping
            
            cropImg2 = cv2.rotate(cropImg, cv2.ROTATE_90_COUNTERCLOCKWISE)

            #cv2.imshow("Original", img)
            #cv2.imshow("Cropped", cropImg2)
            
            ret, jpeg = cv2.imencode('.jpg', cropImg2)
            frame = jpeg.tobytes()
            yield (b'--frame\r\n'
                   b'Content-Type: image/jpeg\r\n\r\n' + frame + b'\r\n')
            key = cv2.waitKey(1)
        cv2.destroyAllWindows()


@app.route('/video_feed')
def video_feed():
    return Response(gen(),
                    mimetype='multipart/x-mixed-replace; boundary=frame')
          

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5001, debug=True) 