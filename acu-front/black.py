# from flask import Flask, render_template, Response
# import cv2
# import dlib

# app = Flask(__name__)

# # Initialize Dlib's face detector and shape predictor
# detector = dlib.get_frontal_face_detector()
# predictor = dlib.shape_predictor("./shape_predictor_68_face_landmarks.dat")

# # Initialize the camera
# cap = cv2.VideoCapture(0)  # Assuming the default camera (you can change this if needed)

# # Check if the camera is opened successfully
# if not cap.isOpened():
#     print("Error: Could not open camera.")
#     exit()

# # Disable mirror image
# cap.set(cv2.CAP_PROP_FPS, 70)

# # Function to generate frames for the video stream
# def generate_frames():
#     while True:
#         ret, frame = cap.read()
#         if not ret:
#             break

#         gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
#         faces = detector(gray)

#         for face in faces:
#             landmarks = predictor(gray, face)

#             for i in range(68):
#                 x, y = landmarks.part(i).x, landmarks.part(i).y
#                 cv2.circle(frame, (x, y), 3, (0, 0, 0), -1)  # 以黑色标记特征点

#         frame_flipped = cv2.flip(frame, 1)
        
#         _, jpeg = cv2.imencode(".jpg", frame_flipped)
#         frame_bytes = jpeg.tobytes()

#         yield (
#             b"--frame\r\n"
#             b"Content-Type: image/jpeg\r\n\r\n" + frame_bytes + b"\r\n\r\n"
#         )

# @app.route("/")
# def index():
#     return render_template("login.html")

# @app.route("/video_feed")
# def video_feed():
#     return Response(
#         generate_frames(), mimetype="multipart/x-mixed-replace; boundary=frame"
#     )

# @app.route("/rehabilitation")
# def rehabilitation():
#     return render_template("front-rehabilitation.html")

# if __name__ == "__main__":
#     app.run(debug=True)

import cv2
import dlib
from aiortc import VideoStreamTrack
from aiortc.contrib.media import MediaStream, MediaStreamTrack
from av import VideoFrame

# Initialize Dlib's face detector and shape predictor
detector = dlib.get_frontal_face_detector()
predictor = dlib.shape_predictor("shape_predictor_68_face_landmarks.dat")

class FaceLandmarksTrack(VideoStreamTrack):
    async def recv(self):
        cap = cv2.VideoCapture(0)  # Assuming the default camera

        while True:
            ret, frame = cap.read()
            if not ret:
                return

            gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
            faces = detector(gray)

            for face in faces:
                landmarks = predictor(gray, face)

                for i in range(68):
                    x, y = landmarks.part(i).x, landmarks.part(i).y
                    cv2.circle(frame, (x, y), 3, (0, 0, 0), -1)  # 以黑色标记特征点

            frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
            img = VideoFrame.from_ndarray(frame_rgb, format="rgb24")
            pts = img.to_bytes()

            frame_time = 1  # Adjust frame time as needed
            await asyncio.sleep(frame_time)

            yield img

async def consume_signaling(pc, signaling):
    pass  # Add your signaling code here

if __name__ == "__main__":
    pc = RTCPeerConnection()
    signaling = YourSignalingClass()
    pc.add_track(FaceLandmarksTrack())
    
    loop = asyncio.get_event_loop()
    try:
        loop.run_until_complete(consume_signaling(pc, signaling))
    except KeyboardInterrupt:
        pass
    finally:
        loop.run_until_complete(pc.close())
