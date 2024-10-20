import 'package:flutter/material.dart';
import 'package:local_auth/local_auth.dart';
import '../services/employee_service.dart';  // Import the EmployeeService
import '../services/device_info_plus.dart';  // Assuming you have device_info_plus in a service

class LoginScreen extends StatefulWidget {
  const LoginScreen({super.key});

  @override
  _LoginScreenState createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen> {
  final LocalAuthentication _localAuth = LocalAuthentication();
  String _authMessage = "Please scan your fingerprint to log in.";
  final EmployeeService _employeeService = EmployeeService(); // Initialize the EmployeeService

  @override
  void initState() {
    super.initState();
    // Start authentication process immediately when the screen loads
    _checkBiometricsAndAuthenticate();
  }

  // Check if biometric authentication is available and perform authentication
  Future<void> _checkBiometricsAndAuthenticate() async {
    bool canCheckBiometrics = await _localAuth.canCheckBiometrics;

    if (!canCheckBiometrics) {
      setState(() {
        _authMessage = "Biometric authentication is not available on this device.";
      });
      return;
    }

    // Proceed with authentication
    _authenticate();
  }

  // Perform fingerprint authentication
  Future<void> _authenticate() async {
    bool isAuthenticated = false;
    try {
      isAuthenticated = await _localAuth.authenticate(
        localizedReason: 'Authenticate with fingerprint to log in',
        options: const AuthenticationOptions(biometricOnly: true),
      );
    } catch (e) {
      print(e);
      setState(() {
        _authMessage = "Error during authentication: $e";
      });
      return;
    }

    if (isAuthenticated) {
      _fetchUserDetails();
    } else {
      setState(() {
        _authMessage = "Fingerprint authentication failed. Please try again.";
      });
    }
  }

  // Fetch user details from backend using device ID
  Future<void> _fetchUserDetails() async {
    String? deviceId = await getDeviceId(); // Assuming this is imported from device_info_plus

    if (deviceId == null) {
      setState(() {
        _authMessage = "Failed to retrieve device ID.";
      });
      return;
    }

    // Use EmployeeService to fetch user details
    final employee = await _employeeService.getEmployeeByDeviceId(deviceId);

    if (employee != null) {
      setState(() {
        _authMessage = "Welcome!"; // Update to use employee name
      });

      // After successful login, navigate to the next screen
      Navigator.pushNamed(context, '/scanner');
    } else {
      setState(() {
        _authMessage = "User not found.";
      });
      Navigator.pushNamed(context, '/scanner');
    }
  }

  // Navigate to the registration screen
  void _navigateToRegister() {
    Navigator.pushNamed(context, '/register');
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
          Center(
            child: SingleChildScrollView(
              padding: const EdgeInsets.all(24.0),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  // Logo or icon
                  Icon(
                    Icons.fingerprint,
                    size: 100,
                    color: Colors.white.withOpacity(0.8),
                  ),
                  const SizedBox(height: 20),
                  // Authentication message
                  Text(
                    _authMessage,
                    style: const TextStyle(
                      fontSize: 18,
                      color: Colors.white,
                      fontWeight: FontWeight.w400,
                    ),
                    textAlign: TextAlign.center,
                  ),
                  const SizedBox(height: 40),
                  // Authenticate button
                  ElevatedButton(
                    onPressed: _checkBiometricsAndAuthenticate,
                    style: ElevatedButton.styleFrom(
                      padding: const EdgeInsets.symmetric(
                          vertical: 14, horizontal: 60),
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(30),
                      ),
                      backgroundColor: Colors.white,
                      foregroundColor: Colors.blueAccent,
                      elevation: 5,
                      shadowColor: Colors.blueAccent.withOpacity(0.5),
                    ),
                    child: const Text(
                      'Authenticate',
                      style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                    ),
                  ),
                  const SizedBox(height: 20),
                  // Navigate to Register
                  TextButton(
                    onPressed: _navigateToRegister,
                    child: const Text(
                      'New user? Register here',
                      style: TextStyle(color: Colors.white, fontSize: 14),
                    ),
                  ),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}
