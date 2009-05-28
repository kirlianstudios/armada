#region

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IUpdateable=Harmony.Components.IUpdateable;

#endregion

namespace Harmony.Cameras
{
    public interface ICamera : IUpdateable
    {
        Viewport Viewport { get; set; }

        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }

        Matrix World { get; }
        Matrix View { get; }
        Matrix Projection { get; }

        float FieldOfView { get; set; }
        Vector3 Target { get; set; }

        void Rotate(Vector3 axis, float angle);

        void Translate(Vector3 distance);

        void Revolve(Vector3 axis, float angle);

        void RotateGlobal(Vector3 axis, float angle);

        void TranslateGlobal(Vector3 distance);

        void RevolveGlobal(Vector3 axis, float angle);
    }
}