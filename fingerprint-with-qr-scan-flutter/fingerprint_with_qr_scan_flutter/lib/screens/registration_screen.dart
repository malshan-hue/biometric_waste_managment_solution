import 'package:flutter/material.dart';
import '../models/employee.dart'; // Import the Employee model
import '../services/employee_service.dart'; // Import the service
import '../services/device_info_plus.dart';

class RegisterScreen extends StatefulWidget {
  const RegisterScreen({super.key});

  @override
  _RegisterScreenState createState() => _RegisterScreenState();
}

class _RegisterScreenState extends State<RegisterScreen> {
  final TextEditingController _fullNameController = TextEditingController();
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _employeeCodeController = TextEditingController();
  final TextEditingController _phoneController = TextEditingController();

  final EmployeeService _employeeService = EmployeeService();

  Future<void> _registerUser() async {
    // Get form values
    String fullName = _fullNameController.text;
    String email = _emailController.text;
    String employeeCode = _employeeCodeController.text;
    String phone = _phoneController.text;

    // Simulate device ID for this example (you can use actual device ID logic as needed)
    String? deviceId = await getDeviceId();  // Replace with your actual device ID retrieval logic

    // Create a new Employee object
    Employee newEmployee = Employee(
      EmployeeId: 0,  // Initially 0, will be set by the backend
      EmployeeCode: employeeCode,
      FullName: fullName,
      Email: email,
      Phone: phone,
      DeviceId: deviceId ?? 'No Id Found',
    );

    // Register the employee using the EmployeeService and handle success/failure
    bool isRegistered = await _employeeService.registerEmployee(newEmployee);

    // Handle the response
    if (isRegistered) {
      // Registration successful
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Registration successful!')),
      );
      // Navigate to another screen or handle success action here
    } else {
      // Registration failed
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Registration failed. Please try again.')),
      );
    }
  }

  // Navigate to the registration screen
  void _navigateToAuth() {
    Navigator.pushNamed(context, '/auth');
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Register')),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          children: [
            TextField(controller: _fullNameController, decoration: const InputDecoration(labelText: 'Full Name')),
            TextField(controller: _emailController, decoration: const InputDecoration(labelText: 'Email')),
            TextField(controller: _employeeCodeController, decoration: const InputDecoration(labelText: 'Employee Code')),
            TextField(controller: _phoneController, decoration: const InputDecoration(labelText: 'Phone')),
            const SizedBox(height: 20),
            ElevatedButton(
              onPressed: _registerUser,
              child: const Text('Register'),
            ),
            ElevatedButton(
              onPressed: _navigateToAuth,
              child: const Text('Auth'),
            ),
          ],
        ),
      ),
    );
  }
}
