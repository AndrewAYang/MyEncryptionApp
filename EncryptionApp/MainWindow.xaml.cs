using SecuredDataEncryption;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace RandomizedEncryptionCodeDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private const string DefaultPassword = "guest";

        public MainWindow()
        {
            InitializeComponent();
            SetupDefaultPlainText();
        }

        private string GetDefaultPlainText()
        {                        
            try
            {
                return ConfigurationManager.AppSettings["DefaultPlainText"];
            }
            catch
            {
                return "";
            }            
        }

        private void SetupDefaultPlainText()
        {
            txtEncryptedCodes.Text = "";
            txtDecryptedText.Text = "";
            txtPlainText.Text = GetDefaultPlainText();            
            passwordBox.Password = DefaultPassword;

            //deselect text to make it more readable
            txtPlainText.SelectionStart = txtPlainText.Text.Length;
            txtPlainText.SelectionLength = 0;
            lblSaveEncryptedText.Content = "";
        }


        /// <summary>
        /// Plain text runs through an encryption algorithm to return encrypted code.
        /// The hash value of the password is used as part of the encryption algorithm.
        /// Password is also needed for decryption.
        /// </summary>
        /// <param name="plaintext"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private string EncryptText(string plaintext, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(plaintext))
                {
                    return null;
                }

                var encryptionProcessor = new EncryptionProcessor(plaintext, password);
                var encryptedText = encryptionProcessor.Encrypt();
                return encryptedText;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Encrypted code, together with the password, is decrypted
        /// to plain text through the decryption algorithm.
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private string DecryptText(string encryptedText, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(encryptedText))
                {
                    return null;
                }

                var decryptionProcessor = new DecryptionProcessor(encryptedText, password);
                var plainText = decryptionProcessor.Decrypt();
                return plainText;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(passwordBox.Password))
            {
                var message = @"Enter a password to encrypt your secret text. You will need this password to decrypt the encrypted codes.";
                MessageBox.Show(message, "Enter a password", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
                                
            }

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            //Enclose the encrypted code inside brackets so user can be sure that
            //they are not leaving any part of the encrypted code out after copying
            txtEncryptedCodes.Text = "{" + EncryptText(txtPlainText.Text, passwordBox.Password) + "}";

            stopWatch.Stop();
            lblEncryptTime.Content = string.Format("(Encrypted in {0})", stopWatch.Elapsed.ToString());

            //Clear old decrypted text if any to minimize confusion
            txtDecryptedText.Text = string.Empty;
            lblSaveEncryptedText.Content = string.Empty;                       
        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(passwordBox.Password))
            {
                var message = @"Enter the password used to encrypt the secret text.";
                MessageBox.Show(message, "Enter a password", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var encryptedText = txtEncryptedCodes.Text.TrimStart('{').TrimEnd('}');

            txtDecryptedText.Text = DecryptText(encryptedText, passwordBox.Password);

            stopWatch.Stop();
            lblDecryptTime.Content = string.Format("(Decrypted in {0})", stopWatch.Elapsed.ToString());
        }

        private void btnSaveEncryptedText_Click(object sender, RoutedEventArgs e)
        {            
            Clipboard.SetText(txtEncryptedCodes.Text);
            lblSaveEncryptedText.Content = "Encrypted codes copied to Clipboard.";
        }

        private void txtPlainText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lblPlainCharacterCount != null && txtPlainText != null && txtPlainText.Text != null)
            {
                lblPlainCharacterCount.Content = string.Format("({0} characters)", txtPlainText.Text.Length);
            }
        }

        private void txtDecryptedText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lblDecryptedCharacterCount != null && txtDecryptedText != null && txtDecryptedText.Text != null)
            {
                lblDecryptedCharacterCount.Content = string.Format("({0} characters)", txtDecryptedText.Text.Length);
            }
        }


        #region UI resize

        /// <summary>
        /// When the main windows is resized, we want some UI controls on the window to 
        /// adjust accordingly.  Not encryption related.
        /// </summary>
        private void ResizeControls()
        {   
            const double minWidth = 960;
            const double minHeight = 720;

            if (this.ActualWidth > minWidth)
            {
                const double encryptedCodesMarginWidth = 80;
                const double textBoxBufferWidth = 14;
                const double marginSize = 84;

                double textBoxWidth = (this.ActualWidth - marginSize) / 2 - textBoxBufferWidth;

                //1) Plaintext and Descrypted Text Areas
                txtPlainText.Width = textBoxWidth;

                double textLeft = txtPlainText.Width + 52;
                txtDecryptedText.Margin = new Thickness(textLeft, txtDecryptedText.Margin.Top, txtDecryptedText.Margin.Right, txtDecryptedText.Margin.Bottom);                
                txtDecryptedText.Width = textBoxWidth;                
                lblDecrypted.Margin = new Thickness(txtDecryptedText.Margin.Left, lblDecrypted.Margin.Top, lblDecrypted.Margin.Right, lblDecrypted.Margin.Bottom);
                lblDecryptedCharacterCount.Margin = new Thickness(lblDecrypted.Margin.Left + 180, lblDecryptedCharacterCount.Margin.Top, lblDecryptedCharacterCount.Margin.Right, lblDecryptedCharacterCount.Margin.Bottom);


                //2) EncryptedCodes and Copy Button
                txtEncryptedCodes.Width = this.ActualWidth - encryptedCodesMarginWidth;
            }
            
            if (this.ActualHeight > minHeight)
            {
                const double encryptedCodesMarginHeight = 90;

                //We want it to only affect the EncryptedCodes text box and Copy button
                //2) EncryptedCodes and Copy Button
                txtEncryptedCodes.Height = this.ActualHeight - 450;
                double yPosition = this.ActualHeight - encryptedCodesMarginHeight;
                panelCopyButton.Margin = new Thickness(panelCopyButton.Margin.Left, yPosition, panelCopyButton.Margin.Right, panelCopyButton.Margin.Bottom);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeControls();
        }

        #endregion
    }
}
