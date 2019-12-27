# A demo of my randomized encryption code algorithm

This Windows WPF app demonstrates a few cool features of an encryption algorithm that I have created. The algorithm is based on the classic RSA algorithm. I have added a few code-based mathematical rules to simulate the "intrinsic randomness" that a robust cryptographic algorithm should have today in order to guard against hackers with powerful computers. This algorithm can encrypt any human language from single-byte character languages, such as English, to double-byte character languages, such as Chinese, to bi-directional languages, such as Hebrew and Arabic.

How does my algorithm work? A random integer is generated and embedded in the encrypted message. The following formula is used to randomize the encrypted code:

Final Encrypted Code = Initial RSA Encrypted Code + Greatest Prime Factor Function (Random Integer + Password hash value + Plaintext Character Position)

Since these mathematical operations are reversible, the encrypted code can be decrypted. I believe it's possible to design some mathematical rules that can achieve a more robust randomness that is hard to hack even with the powerful computers available today. More work needs to be done in this area. 

With my current implementation, the same plaintext character may generate different encrypted code, and different plaintext characters may end up sharing the same encrypted code in the same message. Code unpredictability and duplication provide some value in deterring the hacking with machine learning approaches.

How to run the app:

1) Download the source code. Run it with Visual Studio. The .NET Framework 4.71 or higher is required. 
  
  OR
  
2) In Windows 10, download the source code and run RandomizedEncryptionCodeDemo.exe in \bin\debug

Please note that the source code of my encryption algorithm is not included with this WFP demo app for security reasons. The algorithm may be commercially viable someday. For now, a runtime version of the algorithm DLL is distributed instead.
