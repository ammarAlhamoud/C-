using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Balls.Common.Models
{
    /// <summary>
    /// This class carry the Content Manager, SpriteBatch, Asset Name and Position of a component.
    /// The component may be Ball, Bar, Text etc.,
    /// </summary>
    public class ComponentModel
    {
        public ContentManager ContentManager { get; set; }

        public SpriteBatch SpriteBatch { get; set; }

        public Vector2 Position { get; set; }

        private string _assetName;

        public string AssetName
        {
            get { return _assetName; }
            set
            {
                if (_assetName != value)
                    _texture2D = null;
                _assetName = value;

            }
        }

        private Texture2D _texture2D;
        public Texture2D Texture2D
        {
            get
            {
                if (null == _texture2D)
                    _texture2D = this.ContentManager.Load<Texture2D>(_assetName);
                return _texture2D;
            }
        }

        private SpriteFont _spriteFont;
        public SpriteFont SpriteFont
        {
            get
            {
                if (null == _spriteFont)
                    _spriteFont = this.ContentManager.Load<SpriteFont>(_assetName);
                return _spriteFont;
            }
        }

        public Vector2 Origin
        {
            get
            {
                return new Vector2(Texture2D.Width / 2, Texture2D.Height / 2);
            }
        }

    }
}
