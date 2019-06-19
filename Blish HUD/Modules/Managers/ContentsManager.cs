﻿using System;
using Blish_HUD.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;

namespace Blish_HUD.Modules.Managers {
    public class ContentsManager {

        private readonly IDataReader _reader;

        public ContentsManager(IDataReader reader) {
            _reader = reader;
        }

        /// <summary>
        /// Loads a <see cref="Texture2D"/> from a file such as a PNG.
        /// </summary>
        /// <param name="texturePath">The path to the texture.</param>
        public Texture2D GetTexture(string texturePath) {
            return GetTexture(texturePath, ContentService.Textures.Error);
        }

        /// <summary>
        /// Loads a <see cref="Texture2D"/> from a file such as a PNG. If the requested texture is inaccessible, the <see cref="fallbackTexture"/> will be returned.
        /// </summary>
        /// <param name="texturePath">The path to the texture.</param>
        /// <param name="fallbackTexture">An alternative <see cref="Texture2D"/> to return if the requested texture is not found or is invalid.</param>
        public Texture2D GetTexture(string texturePath, Texture2D fallbackTexture) {
            using (var textureStream = _reader.GetFileStream(texturePath)) {
                if (textureStream != null) {
                    return Texture2D.FromStream(GameService.Graphics.GraphicsDevice, textureStream);
                }
            }

            return fallbackTexture;
        }

        /// <summary>
        /// Loads a compiled shader in from a file as a <see cref="TEffect"/> that inherits from <see cref="Effect"/>.
        /// </summary>
        /// <typeparam name="TEffect">A custom effect wrapper (similar to the function of <see cref="BasicEffect"/>).</typeparam>
        /// <param name="effectPath">The path to the compiled shader.</param>
        public Effect GetEffect<TEffect>(string effectPath) where TEffect : Effect {
            if (GetEffect(effectPath) is TEffect effect) {
                return effect;
            }

            return null;
        }

        /// <summary>
        /// Loads a compiled shader in from a file as an <see cref="Effect"/>.
        /// </summary>
        /// <param name="effectPath">The path to the compiled shader.</param>
        public Effect GetEffect(string effectPath) {
            long effectDataLength = _reader.GetFileBytes(effectPath, out byte[] effectData);

            if (effectDataLength > 0) {
                return new Effect(GameService.Graphics.GraphicsDevice, effectData, 0, (int)effectDataLength);
            }

            return null;
        }

        /// <summary>
        /// Loads a <see cref="SoundEffect"/> from a file.
        /// </summary>
        /// <param name="soundPath">The path to the sound file.</param>
        public SoundEffect GetSound(string soundPath) {
            using (var soundStream = _reader.GetFileStream(soundPath)) {
                if (soundStream != null)
                    return SoundEffect.FromStream(soundStream);
            }

            return null;
        }

        /// <summary>
        /// [NOT IMPLEMENTED] Loads a <see cref="BitmapFont"/> from a file.
        /// </summary>
        /// <param name="fontPath">The path to the font file.</param>
        public BitmapFont GetBitmapFont(string fontPath) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// [NOT IMPLEMENTED] Loads a <see cref="Model"/> from a file.
        /// </summary>
        /// <param name="modelPath">The path to the model.</param>
        public Model GetModel(string modelPath) {
            throw new NotImplementedException();
        }

    }

}