﻿#region

using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Harmony.Cameras
{
    public class TargetCamera : ICamera
    {
        public Viewport m_viewport;

        public TargetCamera()
        {
            Position = new Vector3(0, 0, 1);
            Rotation = new Quaternion(0, 0, 0, 1);

            FieldOfView = MathHelper.Pi/3.0f;
            Target = new Vector3(0, 0, 0);

            Viewport = new Viewport();
        }

        #region ICamera Members

        public float FieldOfView { get; set; }
        public Vector3 Target { get; set; }

        public Id Id { get; set; }

        public Viewport Viewport
        {
            get { return m_viewport; }
            set
            {
                m_viewport = value;
                m_viewport.MinDepth = 1.0f;
                m_viewport.MaxDepth = 1000.0f;
            }
        }

        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        public Matrix World { get; private set; }
        public Matrix View { get; private set; }
        public Matrix Projection { get; private set; }

        public void Update(GameTime a_gameTime)
        {
            Trace.WriteLine("Camera pos: " + Position);
            UpdateMatrices();
        }

        public void Rotate(Vector3 axis, float angle)
        {
            axis = Vector3.Transform(axis, Matrix.CreateFromQuaternion(Rotation));
            Rotation = Quaternion.Normalize(Quaternion.CreateFromAxisAngle(axis, angle)*Rotation);

            UpdateMatrices();
        }

        public void Translate(Vector3 distance)
        {
            Position += Vector3.Transform(distance, Matrix.CreateFromQuaternion(Rotation));

            UpdateMatrices();
        }

        public void Revolve(Vector3 axis, float angle)
        {
            var revolveAxis = Vector3.Transform(axis, Matrix.CreateFromQuaternion(Rotation));
            var rotate = Quaternion.CreateFromAxisAngle(revolveAxis, angle);
            Position = Vector3.Transform(Position - Target, Matrix.CreateFromQuaternion(rotate)) + Target;

            Rotate(axis, angle);
        }

        public void RotateGlobal(Vector3 axis, float angle)
        {
            Rotation = Quaternion.Normalize(Quaternion.CreateFromAxisAngle(axis, angle)*Rotation);

            UpdateMatrices();
        }

        public void TranslateGlobal(Vector3 distance)
        {
            Position += distance;
            UpdateMatrices();
        }

        public void RevolveGlobal(Vector3 axis, float angle)
        {
            var rotate = Quaternion.CreateFromAxisAngle(axis, angle);
            Position = Vector3.Transform(Position - Target, Matrix.CreateFromQuaternion(rotate)) + Target;

            RotateGlobal(axis, angle);
        }

        #endregion

        private void UpdateMatrices()
        {
            World = Matrix.Identity;

            View = Matrix.Invert(Matrix.CreateFromQuaternion(Rotation)*Matrix.CreateTranslation(Position));

            Projection = Matrix.CreatePerspectiveFieldOfView(
                FieldOfView,
                Viewport.AspectRatio,
                Viewport.MinDepth,
                Viewport.MaxDepth
                );
        }
    }
}