using System;
using System.Collections.ObjectModel;
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

            Content.RootDirectory = "Content";

            Components.Add(new GameObjectManager(this));
            Components.Add(new CameraManager(this));
            Components.Add(new EffectManager(this));
            Components.Add(new InputManager(this));
            Components.Add(new ComponentManager(this));

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;

            //var quad = new Quad("Textures/harmony", Color.White);
            //quad.Shader = "TCT";
            //GameObjectManager.AddGameObject("quad", quad);

            var shader = new Shader("Shaders/TransformColorTexture");
            EffectManager.AddEffect("TCT", shader);

            KeyboardDevice = new KeyboardDevice();

            InputManager.AddDevice("keyboard", KeyboardDevice);
            KeyboardDevice.OnKeyReleased += Keyboard_OnKeyReleased;

            Mouse = new MouseDevice();
            InputManager.AddDevice("mouse", Mouse);

            Mouse.OnMouseMoved += Mouse_OnMouseMoved;
            Mouse.OnMouseScrolled += Mouse_OnMouseScrolled;

            GamePad = new GamePadDevice(PlayerIndex.One);

            InputManager.AddDevice("player1", GamePad);
            GamePad.OnButtonReleased += Gamepad_OnButtonReleased;
            GamePad.OnJoystickMoved += Gamepad_OnJoystickMoved;

            string[] sky = {
                               "Textures/Skybox/red_side", "Textures/Skybox/red_side",
                               "Textures/Skybox/red_side", "Textures/Skybox/red_side",
                               "Textures/Skybox/red_side", "Textures/Skybox/black_side"
                           };
            Skybox = new Skybox(sky, Color.White);
            Skybox.Shader = "TCT";
            GameObjectManager.AddGameObject("skybox", Skybox);

            var model = new Model("Models/cube");
            //model.Shader = "TCT";
            model.Position = new Vector3(0, 5, 0);
            GameObjectManager.AddGameObject("cube", model);

            var post = new PostProcessor("Shaders/PostProcessors/Invert");
            EffectManager.AddEffect("PI", post);
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

        private void Mouse_OnMouseScrolled(int ticks)
        {
            CameraManager.ActiveCamera.Translate(new Vector3(0, 0, ticks*0.01f));
        }

        private void Mouse_OnMouseMoved(Vector2 move)
        {
            CameraManager.ActiveCamera.Revolve(new Vector3(1, 0, 0), move.Y*0.01f);
            CameraManager.ActiveCamera.RevolveGlobal(new Vector3(0, 1, 0), move.X*0.01f);
        }

        private void Keyboard_OnKeyReleased(Collection<Keys> keys)
        {
            if (keys.Contains(Keys.F))
            {
                ToggleFullScreen();
            }

            // Allows the game to exit
            if (keys.Contains(Keys.Escape))
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
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO implement fullscreen

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}