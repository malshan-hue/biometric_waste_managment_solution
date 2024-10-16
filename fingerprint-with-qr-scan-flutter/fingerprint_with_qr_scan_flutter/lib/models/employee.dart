class Employee {
  int EmployeeId;
  String EmployeeCode;
  String FullName;
  String Email;
  String Phone;
  String DeviceId;

  Employee({
    required this.EmployeeId,
    required this.EmployeeCode,
    required this.FullName,
    required this.Email,
    required this.Phone,
    required this.DeviceId,
  });

  // Factory constructor to create an Employee object from JSON
  factory Employee.fromJson(Map<String, dynamic> json) {
    return Employee(
      EmployeeId: json['employeeId'] as int,
      EmployeeCode: json['employeeCode'] as String,
      FullName: json['fullName'] as String,
      Email: json['email'] as String,
      Phone: json['phone'] as String,
      DeviceId: json['deviceId'] as String,
    );
  }

  // Method to convert Employee object to JSON
  Map<String, dynamic> toJson() {
    return {
      'employeeId': EmployeeId,
      'employeeCode': EmployeeCode,
      'fullName': FullName,
      'email': Email,
      'phone': Phone,
      'deviceId': DeviceId,
    };
  }
}
