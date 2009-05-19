using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using Harmony.Cameras;
using Harmony.Components;
using Harmony.Effects;
using Harmony.Inputs;
using Harmony.Objects;
using HMEngine.HMObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Model=Harmony.Objects.Model;

namespace Harmony
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game()
        {
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferMultiSampling = true;

            Content.RootDirectory = "Content";

            IsMouseVisible = true;

            Components.Add(new GameObjectManager(this));
            Components.Add(new CameraManager(this));
            Components.Add(new EffectManager(this));
            Components.Add(new InputManager(this));
            Components.Add(new ComponentManager(this));

        // Inputs
            KeyboardDevice = new KeyboardDevice();

            InputManager.AddDevice("keyboard", KeyboardDevice);
            KeyboardDevice.KeyRelease += Keyboard_OnKeyReleased;
            KeyboardDevice.KeyHold += KeyboardDevice_KeyHold;

            // Mouse
            Mouse = new MouseDevice();
            InputManager.AddDevice("mouse", Mouse);
            Mouse.OnMouseMoved += Mouse_OnMouseMoved;
            Mouse.OnMouseScrolled += Mouse_OnMouseScrolled;

            // GamePad[s]
            GamePad = new GamePadDevice(PlayerIndex.One);
            InputManager.AddDevice("player1", GamePad);
            GamePad.OnButtonReleased += Gamepad_OnButtonReleased;
            GamePad.OnJoystickMoved += Gamepad_OnJoystickMoved;

        // Window
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;

        // Skybox
            string[] sky =
            {
                "Textures/Skybox/white_side", "Textures/Skybox/white_side",
                "Textures/Skybox/white_side", "Textures/Skybox/white_side",
                "Textures/Skybox/red_side", "Textures/Skybox/white_side"
            };
            Skybox = new Skybox(sky, Color.White);
            Skybox.Shader = "TCT";
            var shader = new Shader("Shaders/TransformColorTexture");
            EffectManager.AddEffect("TCT", shader);
            GameObjectManager.AddGameObject("skybox", Skybox);

            var model = new Model("Models/ship_greeble");
            ComponentManager.AddComponent("ship", model);

        //// Tile
        //    for (int row = 0; row < 10; row++)
        //    {
        //        for (int column = 0; column < 10; column++)
        //        {
        //            var model = new Model("Models/tile");
        //            //model.Shader = "TCT";
        //            model.Position = new Vector3(row, column, 0);
        //            model.BoundingBox = new BoundingBox(new Vector3(row, 0, column), new Vector3(row*1.0f, 0.1f, row*column));
        //            var quad = new Quad("Textures/Harmony", new Color(((float)row) / 10.0f, ((float)column) / 10.0f, 0));
        //            GameObjectManager.AddGameObject("quad" + row + column, quad);
        //            quad.Position = new Vector3(row, -1.0f, -column);
        //            quad.Shader = "TCT";
        //            quad.Rotation = Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), -((float)Math.PI)/2.0f);
        //            quad.Scale = new Vector3(1.0f);

        //            GameObjectManager.AddGameObject("tile" + row + column, model);
        //        }
        //    }
        }

        void KeyboardDevice_KeyHold(Collection<Keys> a_keys)
        {
            var map = new Dictionary<Keys, Vector3>();
            map[Keys.W] = new Vector3(0, 0, -1);
            map[Keys.S] = new Vector3(0, 0, 1);
            map[Keys.A] = new Vector3(-1, 0, 0);
            map[Keys.D] = new Vector3(1, 0, 0);

            var translation = new Vector3();

            foreach(var key in map.Keys)
            {
                if (a_keys.Contains(key))
                {
                    translation += map[key];
                }
            }

            CameraManager.ActiveCamera.Translate(translation);
        }

        private GamePadDevice GamePad { get; set; }
        private KeyboardDevice KeyboardDevice { get; set; }
        private MouseDevice Mouse { get; set; }
        private Skybox Skybox { get; set; }

        /// <summary>
        /// Gets or sets the graphics.
        /// </summary>
        /// <value>The graphics.</value>
        private GraphicsDeviceManager Graphics { get; set; }

        /// <summary>
        /// Gets or sets the sprite batch.
        /// </summary>
        /// <value>The sprite batch.</value>
        private SpriteBatch SpriteBatch { get; set; }

        private void Gamepad_OnButtonReleased(Collection<GamePadButton> buttons)
        {
            if (buttons.Contains(GamePadButton.Back))
            {
                Exit();
            }
        }

        private static void Gamepad_OnJoystickMoved(Vector2 leftStick, Vector2 rightStick)
        {
            CameraManager.ActiveCamera.Revolve(new Vector3(1, 0, 0), leftStick.Y*0.1f);
            CameraManager.ActiveCamera.RevolveGlobal(new Vector3(0, 1, 0), leftStick.X*0.1f);
            CameraManager.ActiveCamera.Translate(new Vector3(0, 0, -rightStick.Y*0.5f));
        }

        private void Mouse_OnMouseScrolled(int a_clicks)
        {
            CameraManager.ActiveCamera.Translate(new Vector3(0, 0, a_clicks*0.01f));
        }

        private void Mouse_OnMouseMoved(Vector2 a_movement)
        {
            //CameraManager.ActiveCamera.Revolve(new Vector3(1, 0, 0), a_movement.Y*0.01f);
            //CameraManager.ActiveCamera.RevolveGlobal(new Vector3(0, 1, 0), a_movement.X*0.01f);

            CameraManager.ActiveCamera.Rotate(new Vector3(1, 0, 0), -1 * a_movement.Y * 0.01f);
            CameraManager.ActiveCamera.RotateGlobal(new Vector3(0, 1, 0), -1 * a_movement.X * 0.01f);
        }

        private void Keyboard_OnKeyReleased(Collection<Keys> a_keys)
        {
            if (a_keys.Contains(Keys.F))
            {
                ToggleFullScreen();
            }

            // Allows the game to exit
            if (a_keys.Contains(Keys.Escape))
            {
                Exit();
            }
        }

        public void ToggleFullScreen()
        {
            Graphics.ToggleFullScreen();
        }

        protected virtual void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            Graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            Graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;

            CameraManager.ActiveCamera.Viewport = GraphicsDevice.Viewport;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="a_gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime a_gameTime)
        {
            // TODO implement fullscreen

            // TODO: Add your update logic here

            foreach(var pickable in ComponentManager.Pickables.Values)
            {
                pickable.SetSelected(false);

                if (pickable is Model)
                {
                    var model = (Model)pickable;
                    model.DiffuseColor = new Vector3(1, 1, 1);
                }
            }

            var state = Microsoft.Xna.Framework.Input.Mouse.GetState();
            var picked = RayPicker.GetPickables(new Point(state.X, state.Y));
            Debug.WriteLine(state.X + " : " + state.Y);

            foreach(var pickable in picked)
            {
                if (pickable is Model)
                {
                    var model = (Model) pickable;
                    model.DiffuseColor = new Vector3(1, 0, 0);
                }
            }

            base.Update(a_gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="a_gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime a_gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(a_gameTime);
        }
    }
}