using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Caster.WeChat.Common
{
    public class MessageCryptography
    {
        public static uint HostToNetworkOrder(uint input)
        {
            uint result = 0;
            for (int i = 0; i < 4; i++)
                result = (result << 8) + ((input >> (i * 8)) & 255);
            return result;
        }

        public static int HostToNetworkOrder(int input)
        {
            int result = 0;
            for (var i = 0; i < 4; i++)
                result = (result << 8) + ((input >> (i * 8)) & 255);
            return result;
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="input">密文</param>
        /// <param name="encodingAesKey"></param>
        /// <returns></returns>
        /// 
        public static (string, string) AesDecrypt(string input, string encodingAesKey)
        {
            byte[] key;
            key = Convert.FromBase64String(encodingAesKey + "=");
            byte[] iv = new byte[16];
            Array.Copy(key, iv, 16);
            byte[] btmpMsg = AES_decrypt(input, iv, key);

            int len = BitConverter.ToInt32(btmpMsg, 16);
            len = IPAddress.NetworkToHostOrder(len);


            byte[] bMsg = new byte[len];
            byte[] bAppid = new byte[btmpMsg.Length - 20 - len];
            Array.Copy(btmpMsg, 20, bMsg, 0, len);
            Array.Copy(btmpMsg, 20 + len, bAppid, 0, btmpMsg.Length - 20 - len);
            string oriMsg = Encoding.UTF8.GetString(bMsg);
            string appid = Encoding.UTF8.GetString(bAppid);


            return (oriMsg, appid);
        }
        
        
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encodingAesKey"></param>
        /// <param name="appid"></param>
        /// <returns></returns>
        public static string AesEncrypt(string input, string encodingAesKey, string appid)
        {
            byte[] Key;
            Key = Convert.FromBase64String(encodingAesKey + "=");
            byte[] iv = new byte[16];
            Array.Copy(Key, iv, 16);
            string randCode = CreateRandCode(16);
            byte[] bRand = Encoding.UTF8.GetBytes(randCode);
            byte[] bAppid = Encoding.UTF8.GetBytes(appid);
            byte[] bmpMsg = Encoding.UTF8.GetBytes(input);
            byte[] bMsgLen = BitConverter.GetBytes(HostToNetworkOrder(bmpMsg.Length));
            byte[] bMsg = new byte[bRand.Length + bMsgLen.Length + bAppid.Length + bmpMsg.Length];

            Array.Copy(bRand, bMsg, bRand.Length);
            Array.Copy(bMsgLen, 0, bMsg, bRand.Length, bMsgLen.Length);
            Array.Copy(bmpMsg, 0, bMsg, bRand.Length + bMsgLen.Length, bmpMsg.Length);
            Array.Copy(bAppid, 0, bMsg, bRand.Length + bMsgLen.Length + bmpMsg.Length, bAppid.Length);

            return AES_encrypt(bMsg, iv, Key);
        }

        private static string CreateRandCode(int codeLen)
        {
            string codeSerial = "2,3,4,5,6,7,a,c,d,e,f,h,i,j,k,m,n,p,r,s,t,A,C,D,E,F,G,H,J,K,M,N,P,Q,R,S,U,V,W,X,Y,Z";
            if (codeLen == 0)
            {
                codeLen = 16;
            }

            string[] arr = codeSerial.Split(',');
            string code = "";
            int randValue = -1;
            Random rand = new Random(unchecked((int) DateTime.Now.Ticks));
            for (int i = 0; i < codeLen; i++)
            {
                randValue = rand.Next(0, arr.Length - 1);
                code += arr[randValue];
            }

            return code;
        }

        private static string AesEncrypt(string input, byte[] iv, byte[] key)
        {
            var aes = new RijndaelManaged();
            //秘钥的大小，以位为单位
            aes.KeySize = 256;
            //支持的块大小
            aes.BlockSize = 128;
            //填充模式
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            aes.Key = key;
            aes.IV = iv;
            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] xBuff = null;

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Encoding.UTF8.GetBytes(input);
                    cs.Write(xXml, 0, xXml.Length);
                }

                xBuff = ms.ToArray();
            }

            String output = Convert.ToBase64String(xBuff);
            return output;
        }

        private static string AES_encrypt(byte[] input, byte[] iv, byte[] key)
        {
            var aes = new RijndaelManaged();
            //秘钥的大小，以位为单位
            aes.KeySize = 256;
            //支持的块大小
            aes.BlockSize = 128;
            //填充模式
            //aes.Padding = PaddingMode.PKCS7;
            aes.Padding = PaddingMode.None;
            aes.Mode = CipherMode.CBC;
            aes.Key = key;
            aes.IV = iv;
            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] xBuff = null;

            #region 自己进行PKCS7补位，用系统自己带的不行

            byte[] msg = new byte[input.Length + 32 - input.Length % 32];
            Array.Copy(input, msg, input.Length);
            byte[] pad = Kcs7Encoder(input.Length);
            Array.Copy(pad, 0, msg, input.Length, pad.Length);

            #endregion

            #region 注释的也是一种方法，效果一样

            //ICryptoTransform transform = aes.CreateEncryptor();
            //byte[] xBuff = transform.TransformFinalBlock(msg, 0, msg.Length);

            #endregion

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    cs.Write(msg, 0, msg.Length);
                }

                xBuff = ms.ToArray();
            }

            var output = Convert.ToBase64String(xBuff);
            return output;
        }

        private static byte[] Kcs7Encoder(int textLength)
        {
            int blockSize = 32;
            // 计算需要填充的位数
            int amountToPad = blockSize - (textLength % blockSize);
            if (amountToPad == 0)
            {
                amountToPad = blockSize;
            }

            // 获得补位所用的字符
            char padChr = Chr(amountToPad);
            string tmp = "";
            for (int index = 0; index < amountToPad; index++)
            {
                tmp += padChr;
            }

            return Encoding.UTF8.GetBytes(tmp);
        }

        /**
         * 将数字转化成ASCII码对应的字符，用于对明文进行补码
         * 
         * @param a 需要转化的数字
         * @return 转化得到的字符
         */
        static char Chr(int a)
        {
            byte target = (byte) (a & 0xFF);
            return (char) target;
        }

        private static byte[] AES_decrypt(string input, byte[] iv, byte[] key)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;
            aes.Key = key;
            aes.IV = iv;
            var decrypt = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Convert.FromBase64String(input);
                    byte[] msg = new byte[xXml.Length + 32 - xXml.Length % 32];
                    Array.Copy(xXml, msg, xXml.Length);
                    cs.Write(xXml, 0, xXml.Length);
                }

                xBuff = Decode2(ms.ToArray());
            }

            return xBuff;
        }

        private static byte[] Decode2(byte[] decrypted)
        {
            int pad = (int) decrypted[decrypted.Length - 1];
            if (pad < 1 || pad > 32)
            {
                pad = 0;
            }

            byte[] res = new byte[decrypted.Length - pad];
            Array.Copy(decrypted, 0, res, 0, decrypted.Length - pad);
            return res;
        }
    }
}