import 'package:flutter/material.dart';
import 'screens/login_screen.dart';
import './screens/qr_scanner_screen.dart';
import './screens/registration_screen.dart';

void main() {
  runApp(const MainApp());
}

class MainApp extends StatelessWidget {
  const MainApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      initialRoute: '/auth',
      routes: {
        '/register': (context) => const RegisterScreen(),
        '/auth': (context) => const LoginScreen(),
        '/scanner': (context) => const QRScannerScreen(),
      },
    );
  }
}
