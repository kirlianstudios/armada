#region

using Harmony;
using Harmony.Objects;
using Microsoft.Xna.Framework;
using Game=Harmony.Game;

#endregion

namespace Armada
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public sealed class ArmadaGame : Game
    {
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            //System.Reflection.Assembly.GetAssembly().CreateInstance("")
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

            var model = new Model("Models/tile");
            var id = new Id {Handle = "tile", Guid = GuidManager.NewGuid()};
            ModelManager.AddModel(id, model);

            //var quad = new Quad("Textures/harmony", Color.White);
            //var id = new Id() {Handle = "quad", Guid = GuidManager.NewGuid()};
            //GameObjectManager.AddGameObject(id, quad);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
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
        /// <param name="aGameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime a_gameTime)
        {
            // TODO: Add your update logic here

            base.Update(a_gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="aGameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime aGameTime)
        {
            // TODO: Add your drawing code here

            base.Draw(aGameTime);
        }
    }
}