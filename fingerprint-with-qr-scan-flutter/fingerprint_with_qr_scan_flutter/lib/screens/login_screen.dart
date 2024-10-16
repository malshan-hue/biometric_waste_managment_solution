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
      appBar: AppBar(title: const Text('Login')),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text(_authMessage),
            const SizedBox(height: 20),
            ElevatedButton(
              onPressed: _checkBiometricsAndAuthenticate,
              child: const Text('Authenticate'),
            ),
            const SizedBox(height: 20),
            ElevatedButton(
              onPressed: _navigateToRegister,
              child: const Text('Register'),
            ),
          ],
        ),
      ),
    );
  }
}
