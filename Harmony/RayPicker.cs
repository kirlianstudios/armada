using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony.Cameras;
using Harmony.Components;
using Microsoft.Xna.Framework;

namespace Harmony
{
    class RayPicker
    {
       private static Ray Ray { get; set; }
       private static List<Ray> HitRays { get; set; }

       private RayPicker() { }

       public static List<IPickable> GetPickables(Point a_clickPoint)
       {
           var camera = CameraManager.ActiveCamera;

           Vector3 nearSource = camera.Viewport.Unproject(new Vector3(a_clickPoint.X, a_clickPoint.Y, camera.Viewport.MinDepth), camera.Projection, camera.View, Matrix.Identity);
           Vector3 farSource = camera.Viewport.Unproject(new Vector3(a_clickPoint.X, a_clickPoint.Y, camera.Viewport.MaxDepth), camera.Projection, camera.View, Matrix.Identity);
           Vector3 direction = farSource - nearSource;

           direction.Normalize();

           var ray = new Ray(nearSource, direction);

           var pickables = new List<IPickable>();
           var rays = new List<Ray>();
           var map = new Dictionary<float?, IPickable>();

           foreach (var pickable in ComponentManager.Pickables.Values)
           {
               BoundingBox boundingBox = pickable.GetBoundingBox();
               float? distance;
               ray.Intersects(ref boundingBox, out distance);
               if (distance != null)
               {
                   map[distance] = pickable;
                   pickables.Add(pickable);
               }
           }

           return pickables;    
       }      
    }
}
