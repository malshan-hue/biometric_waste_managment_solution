import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/employee.dart'; // Import the Employee model

class EmployeeService {
  final String baseUrl = 'https://3dc3-124-43-209-182.ngrok-free.app/api';  // Update with your actual base API URL

  // Register a new employee and return a boolean indicating success or failure
  Future<bool> registerEmployee(Employee employee) async {
    // Convert the Employee object to JSON
    Map<String, dynamic> employeeData = employee.toJson();

    try {
      // Send registration data to the backend
      final response = await http.post(
        Uri.parse('$baseUrl/Employee/Register'),  // Append the correct API endpoint
        body: jsonEncode(employeeData),
        headers: {'Content-Type': 'application/json'},
      );

      // Return true if registration was successful (status code 200)
      if (response.statusCode == 200) {
        return true;
      } else {
        return false;
      }
    } catch (error) {
      return false;
    }
  }

  // Fetch user details using device ID
  Future<Map<String, dynamic>?> getEmployeeByDeviceId(String deviceId) async {
  try {
    // Include the deviceId as a query parameter
    final response = await http.get(
      Uri.parse('$baseUrl/Employee/GetEmployeeByDeviceId?deviceId=$deviceId'),
      headers: {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    } else {
      // Log or handle different response status codes as needed
      print('Failed to get employee: ${response.statusCode}');
      return null;
    }
  } catch (error) {
    print('Error fetching employee: $error');
    return null;
  }
}

}
