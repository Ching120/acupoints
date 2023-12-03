from flask import Flask, render_template, Response
import cv2
import dlib

app = Flask(__name__)

# Initialize Dlib's face detector and shape predictor
detector = dlib.get_frontal_face_detector()
predictor = dlib.shape_predictor("../shape_predictor_68_face_landmarks.dat")

# Initialize the camera
cap = cv2.VideoCapture(0)  # Assuming the default camera (you can change this if needed)

# Check if the camera is opened successfully
if not cap.isOpened():
    print("Error: Could not open camera.")
    exit()

# Disable mirror image
cap.set(cv2.CAP_PROP_FPS, 70)

# Function to generate frames for the video stream
def generate_frames():
    while True:
        ret, frame = cap.read()
        if not ret:
            break

        gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
        faces = detector(gray)

        for face in faces:
            landmarks = predictor(gray, face)

            # 魚腰穴（右邊）
            fish_waist_right_x = landmarks.part(19).x
            fish_waist_right_y = landmarks.part(18).y

            # 魚腰穴（左邊）
            fish_waist_left_x = landmarks.part(24).x
            fish_waist_left_y = landmarks.part(25).y

            # 標記
            cv2.circle(frame, (fish_waist_right_x, fish_waist_right_y), 3, (0, 255, 0), -1)
            cv2.circle(frame, (fish_waist_left_x, fish_waist_left_y), 3, (0, 255, 0), -1)


            # 攢竹穴（右邊）
            zanzhu_right_x = landmarks.part(21).x
            zanzhu_right_y = landmarks.part(21).y

            # 攢竹穴（左邊）
            zanzhu_left_x = landmarks.part(22).x
            zanzhu_left_y = landmarks.part(22).y

            cv2.circle(frame, (zanzhu_right_x, zanzhu_right_y), 3, (0, 0, 255), -1)
            cv2.circle(frame, (zanzhu_left_x, zanzhu_left_y), 3, (0, 0, 255), -1)

            # 睛明穴（右眼）
            jingming_right_x = landmarks.part(21).x
            jingming_right_y = landmarks.part(38).y

            # 睛明穴（左眼）
            jingming_left_x = landmarks.part(22).x
            jingming_left_y = landmarks.part(43).y

            # 標記
            cv2.circle(frame, (jingming_right_x, jingming_right_y), 3, (0, 255, 0), -1)
            cv2.circle(frame, (jingming_left_x, jingming_left_y), 3, (0, 255, 0), -1)

            # 瞳子髎穴（右眼）
            tongziliao_right_x = landmarks.part(18).x
            tongziliao_right_y = landmarks.part(0).y

            # 瞳子髎穴（左眼）
            tongziliao_left_x = landmarks.part(25).x
            tongziliao_left_y = landmarks.part(16).y

            # 繪製標記
            cv2.circle(frame, (tongziliao_right_x, tongziliao_right_y), 3, (0, 0, 255), -1)
            cv2.circle(frame, (tongziliao_left_x, tongziliao_left_y), 3, (0, 0, 255), -1)

            # 四白穴（右眼）
            si_bai_right_x = landmarks.part(44).x
            si_bai_right_y = landmarks.part(15).y

            # 四白穴（左眼）
            si_bai_left_x = landmarks.part(37).x
            si_bai_left_y = landmarks.part(15).y

            # 繪製標記
            cv2.circle(frame, (si_bai_right_x, si_bai_right_y), 3, (255, 0, 0), -1)
            cv2.circle(frame, (si_bai_left_x, si_bai_left_y), 3, (255, 0, 0), -1)

        
        frame_flipped = cv2.flip(frame, 1)
        
        _, jpeg = cv2.imencode(".jpg", frame_flipped)
        frame_bytes = jpeg.tobytes()


        yield (
            b"--frame\r\n"
            b"Content-Type: image/jpeg\r\n\r\n" + frame_bytes + b"\r\n\r\n"
        )

@app.route("/")
def index():
    return render_template("login.html")

@app.route("/video_feed")
def video_feed():
    return Response(
        generate_frames(), mimetype="multipart/x-mixed-replace; boundary=frame"
    )

@app.route("/rehabilitation")
def rehabilitation():
    return render_template("front-rehabilitation.html")

if __name__ == "__main__":
    app.run(debug=True)
