﻿using Microsoft.Xna.Framework.Graphics;

namespace Blish_HUD {
    public readonly ref struct GraphicsDeviceContext {

        private readonly GraphicsService _service;

        /// <summary>
        /// Constructs a new graphics device context, that automatically
        /// calls <see cref="GraphicsService.LendGraphicsDevice(bool)"/> on creation
        /// and <see cref="GraphicsService.ReturnGraphicsDevice(string)"/> when disposed.
        /// </summary>
        /// <param name="service">The graphics service instance to use.</param>
        /// <param name="highPriority">A value indicating whether to acquire a high priority instance.</param>
        internal GraphicsDeviceContext(GraphicsService service, bool highPriority) {
            _service = service;
            GraphicsDevice = _service.LendGraphicsDevice(highPriority);
        }

        /// <summary>
        /// Get the GraphicsDevice associated with this context.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; }

        /// <summary>
        /// Disposes of this graphics context, calling <see cref="GraphicsService.ReturnGraphicsDevice(string)"/>
        /// </summary>
        public void Dispose() {
            _service.ReturnGraphicsDevice();
        }
    }
}