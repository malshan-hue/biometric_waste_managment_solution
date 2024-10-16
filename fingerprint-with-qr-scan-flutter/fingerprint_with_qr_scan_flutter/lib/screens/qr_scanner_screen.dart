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

    final url = Uri.parse('https://yourapi.com/endpoint'); // Your API endpoint
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
        apiStatus = 'API call failed: ${response.statusCode}';
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
      appBar: AppBar(title: const Text('QR Scanner')),
      body: Column(
        children: <Widget>[
          Expanded(
            flex: 4,
            child: Center(
              child: isScanning
                  ? Container(
                      width: 250,  // Set the width of the camera view
                      height: 250, // Set the height of the camera view
                      decoration: BoxDecoration(
                        border: Border.all(color: Colors.blue, width: 2), // Optional: Add a border for better visualization
                        borderRadius: BorderRadius.circular(12), // Optional: Add rounded corners
                      ),
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
                    )
                  : Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        ElevatedButton(
                          onPressed: isProcessing ? null : _startScanning,
                          child: const Text('Scan'),
                        ),
                        if (scanResult.isNotEmpty) Padding(
                          padding: const EdgeInsets.all(8.0),
                          child: Text(scanResult, style: TextStyle(fontSize: 16, color: Colors.green)),
                        ),
                        if (apiStatus.isNotEmpty) Padding(
                          padding: const EdgeInsets.all(8.0),
                          child: Text(apiStatus, style: TextStyle(fontSize: 16, color: apiStatus.contains('successful') ? Colors.green : Colors.red)),
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
      floatingActionButton: isScanning
          ? FloatingActionButton(
              onPressed: () => cameraController.toggleTorch(),
              child: const Icon(Icons.flash_on),
            )
          : null,
    );
  }
}
