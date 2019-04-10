using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class EmailNotification
    {
        /// <summary>
        /// Gets or sets from email.
        /// </summary>
        /// <value>From email.</value>
        public string FromEmail { get; set; }

        /// <summary>
        /// Gets or sets the name of the email host.
        /// </summary>
        /// <value>The name of the email host.</value>
        public string EmailHostName { get; set; }

        /// <summary>
        /// Gets or sets the email port.
        /// </summary>
        /// <value>The email port.</value>
        public int EmailPort { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [email enable SSL].
        /// </summary>
        /// <value><c>true</c> if [email enable SSL]; otherwise, <c>false</c>.</value>
        public bool EmailEnableSsl { get; set; }

        /// <summary>
        /// Gets or sets the email user-name.
        /// </summary>
        /// <value>The email user name.</value>
        public string EmailUsername { get; set; }

        /// <summary>
        /// Gets or sets the email password.
        /// </summary>
        /// <value>The email password.</value>
        public string EmailPassword { get; set; }

        /// <summary>
        /// Gets or sets from name.
        /// </summary>
        /// <value>From Name</value>
        public string FromName { get; set; }
    }

}
