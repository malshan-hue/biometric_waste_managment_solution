import 'package:flutter/material.dart';
import 'package:mobile_scanner/mobile_scanner.dart'; // Import the mobile_scanner package
import 'dart:convert'; // For handling JSON data
import 'package:http/http.dart' as http; // To send requests to the API

class QRScannerScreen extends StatefulWidget {
  const QRScannerScreen({super.key});

  @override
  State<StatefulWidget> createState() => _QRScannerScreenState();
}

class _QRScannerScreenState extends State<QRScannerScreen> {
  bool isProcessing = false;
  bool isScanning = false;
  String scanResult = '';
  String apiStatus = '';
  MobileScannerController cameraController = MobileScannerController();

  @override
  void dispose() {
    cameraController.dispose();
    super.dispose();
  }

  Future<void> _processQRCode(String qrData) async {
    setState(() {
      apiStatus = 'Waiting for API response...';
    });

    final url = Uri.parse('https://3dc3-124-43-209-182.ngrok-free.app');
    final response = await http.post(
      url,
      body: json.encode({'qrData': qrData}),
      headers: {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      // API call was successful
      setState(() {
        apiStatus = 'API call successful!';
        scanResult = qrData;
      });
    } else {
      // Handle error
      setState(() {
        apiStatus = 'API call successful!';
      });
    }

    setState(() {
      isProcessing = false;
      isScanning = false; // Go back to normal state
    });
  }

  void _startScanning() {
    setState(() {
      isScanning = true;
      scanResult = '';
      apiStatus = '';
    });

    cameraController.start(); // Start the camera
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.grey[100],
      body: Stack(
        children: [
          // Gradient background
          Container(
            decoration: BoxDecoration(
              gradient: LinearGradient(
                colors: [Colors.blueAccent, Colors.lightBlueAccent],
                begin: Alignment.topLeft,
                end: Alignment.bottomRight,
              ),
            ),
          ),
          Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              Expanded(
                flex: 4,
                child: Center(
                  child: isScanning
                      ? Container(
                          width: 250,
                          height: 250,
                          decoration: BoxDecoration(
                            border: Border.all(color: Colors.white, width: 3), // White border for the camera preview
                            borderRadius: BorderRadius.circular(16), // Rounded corners
                            boxShadow: [
                              BoxShadow(
                                color: Colors.black.withOpacity(0.3),
                                blurRadius: 10,
                                spreadRadius: 5,
                              )
                            ],
                          ),
                          child: ClipRRect(
                            borderRadius: BorderRadius.circular(16),
                            child: MobileScanner(
                              controller: cameraController,
                              onDetect: (capture) {
                                final List<Barcode> barcodes = capture.barcodes;

                                for (final barcode in barcodes) {
                                  if (barcode.rawValue != null && !isProcessing) {
                                    setState(() {
                                      isProcessing = true;
                                      scanResult = 'Scanned: ${barcode.rawValue!}';
                                    });

                                    // Process the first QR code found
                                    String qrData = barcode.rawValue!;
                                    _processQRCode(qrData).then((_) {
                                      setState(() {
                                        isProcessing = false;
                                      });
                                    });

                                    // Stop after the first QR code is processed
                                    break;
                                  }
                                }
                              },
                            ),
                          ),
                        )
                      : Column(
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: [
                            ElevatedButton(
                              onPressed: isProcessing ? null : _startScanning,
                              style: ElevatedButton.styleFrom(
                                padding: const EdgeInsets.symmetric(vertical: 14, horizontal: 40),
                                shape: RoundedRectangleBorder(
                                  borderRadius: BorderRadius.circular(30),
                                ),
                                backgroundColor: Colors.white,
                                foregroundColor: Colors.blueAccent,
                                elevation: 5,
                                shadowColor: Colors.blueAccent.withOpacity(0.5),
                              ),
                              child: const Text(
                                'Start Scanning',
                                style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                              ),
                            ),
                            const SizedBox(height: 20),
                            if (scanResult.isNotEmpty)
                              Padding(
                                padding: const EdgeInsets.all(8.0),
                                child: Text(
                                  scanResult,
                                  style: const TextStyle(fontSize: 16, color: Color.fromARGB(255, 255, 255, 255)),
                                ),
                              ),
                            if (apiStatus.isNotEmpty)
                              Padding(
                                padding: const EdgeInsets.all(8.0),
                                child: Text(
                                  apiStatus,
                                  style: TextStyle(
                                      fontSize: 16,
                                      color: apiStatus.contains('successful') ? const Color.fromARGB(255, 132, 228, 135) : const Color.fromARGB(255, 253, 253, 253)),
                                ),
                              ),
                            if (isProcessing)
                              const Padding(
                                padding: EdgeInsets.all(8.0),
                                child: CircularProgressIndicator(),
                              ),
                          ],
                        ),
                ),
              ),
            ],
          ),
        ],
      ),
      floatingActionButton: isScanning
          ? FloatingActionButton(
              onPressed: () => cameraController.toggleTorch(),
              backgroundColor: Colors.white,
              child: const Icon(Icons.flash_on, color: Colors.blueAccent),
            )
          : null,
    );
  }
}
