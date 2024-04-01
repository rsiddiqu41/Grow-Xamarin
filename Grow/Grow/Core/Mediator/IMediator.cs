using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Core.Mediator
{
    public interface IMediator
    {
        #region Register

        /// <summary>
        /// Registers a callback method, with no parameter, to be invoked when a specific message is broadcasted.
        /// </summary>
        /// <param name="message">The message to register for.</param>
        /// <param name="callback">The callback to be called when this message is broadcasted.</param>
        void Register(string message, Action callback);
        void UnRegister(string message, Action callback);

        /// <summary>
        /// Registers a callback method, with a parameter, to be invoked when a specific message is broadcasted.
        /// </summary>
        /// <param name="message">The message to register for.</param>
        /// <param name="callback">The callback to be called when this message is broadcasted.</param>
        void Register<T>(string message, Action<T> callback);
        void UnRegister<T>(string message, Action<T> callback);
        #endregion // Register

        #region NotifyColleagues

        /// <summary>
        /// Notifies all registered parties that a message is being broadcast.
        /// </summary>
        /// <param name="message">The message to broadcast.</param>
        /// <param name="parameter">The parameter to pass together with the message.</param>
        void NotifyColleagues(string message, object parameter);

        /// <summary>
        /// Notifies all registered parties that a message is being broadcast.
        /// </summary>
        /// <param name="message">The message to broadcast.</param>
        void NotifyColleagues(string message);
        #endregion
    }
}
