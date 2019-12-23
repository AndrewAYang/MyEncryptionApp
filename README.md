# A demo of my randomized encryption code algorithm

This Windows WPF app demonstrates a few cool features of an encryption algorithm that I have created. The algorithm is based on the classic RSA. I have added a few code-based mathematical rules to simulate "intrinsic randomness" that a robust cryptographic algorithm should have today in order to guard against the hackers with powerful computers. This algorithm can encrypt any human language, from single-byte character languages such as English, to double-byte character languages such as Chinese, to bi-directional languages such as Hebrew and Arabic.

How does my algorithm work? A random integer is generated and embedded in the encrypted message. The random integer is added to each encrypted code, along with the greatest prime factor of the sum of each encrypted character, its sequence in the message, and the hash value of the password, as expressed below.

Final Encrypted Code = The Random Integer + GPF (Initial Encrypted Code + Sequence + the hash value of the password)

These mathematical operations are reversible; hence, the encrypted code can be decrypted. Moreover, I believe it's possible to come up with some mathematical rules that can achieve a more secured randomness that is hard to hack even with the most powerful computers available today. More work needs to be done in that area. With my current implementation, the same plaintext character may generate different encrypted code. And different plaintext characters may end up sharing the same encrypted code in the same message. Code unpredictability and duplication provide some value in deterring hacking with machine learning approaches.

How to run the app

1) Download the source code. Run it with Visual Studio. The .NET Framework 4.71 or higher is required. 
  
  OR
  
2) In Windows 10, download the source code and run RandomizedEncryptionCodeDemo.exe in \bin\debug

Please note that the source code of my encryption algorithm is not included with this WFP demo app for security concern. The algorithm may be commercially viable someday. For now, a runtime version of the algorithm DLL is distributed instead.
