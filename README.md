# Weather App

## Overview
The Weather App is a sophisticated web application that provides users with personalized and seamless access to weather information. Built using modern technologies like Blazor, MudBlazor, and MongoDB, the app ensures a reliable and user-friendly experience.

## Features

### 1. Account Registration
- **Registration Page**: 
  - Users create an account by entering their email address, a password, and confirming their password.
  - Passwords are securely hashed and stored in MongoDB.
- **Email Confirmation**:
  - After registration, a confirmation email is sent to the user's email address.
  - The user must click the link in the email to verify their email address, which activates their account.

### 2. User Login
- **Login Page**: 
  - Users log in using their registered email and password.
  - The app authenticates the user against the stored credentials in MongoDB.
- **Home Page Access**:
  - Upon successful login, users are redirected to the Home Page, the central hub for weather features.

### 3. Home Page
- **City Entry**: 
  - Users enter the name of a city to fetch weather information for that location.
- **Fetch Weather Button**:
  - Retrieves current weather details for the entered city.
  - The background image changes based on the weather description to visually represent the weather conditions.

### 4. Weather Forecast
- **Weather Forecast Button**:
  - Navigates users to the Weather Forecast page for detailed predictions.
- **Weather Forecast Page**:
  - Allows users to select a city and a date to view the weather forecast for that day.
  - Displays a three-day forecast to help users plan ahead.
- **Back to Home Button**:
  - Provides easy navigation back to the Home Page.

### 5. Favorite Cities
- **Favorite Cities Button**:
  - Navigates users to the Favorite Cities page, where they can manage their list of preferred locations.
- **Favorite Cities Page**:
  - Users can add cities to their favorites list, which is stored in MongoDB for persistence.
  - Displays current weather details for each favorite city.
  - Users can remove cities from their favorites list.
- **Back to Home Button**:
  - Returns users to the Home Page.

### 6. Logout
- **Logout Button**:
  - Logs users out of their account and clears session data, redirecting them to the login page.

### 7. Forgot Password
- **Forgot Password Link**:
  - Located on the Login Page, it allows users to reset their password.
- **Password Reset Process**:
  - Users enter their email address, and a password reset link is sent to them.
  - The link leads to a page where they can set a new password.

## Technologies Used
- **Blazor**: For building a modern and responsive user interface.
- **MudBlazor**: To enhance the UI with a seamless and consistent experience.
- **MongoDB**: A NoSQL database used for flexible and scalable data storage.

## Design Patterns
- **Repository Pattern**: Ensures clean and maintainable code by separating data access logic from business logic.
- **Error-Handling Mechanisms**: Robust error handling for a reliable user experience.

## Conclusion
The Weather App is a comprehensive tool that provides accurate weather information while allowing for personalization through features like favorite cities. The architecture is designed for maintainability and scalability, making it a reliable solution for users.

## Setup Instructions
1. **Clone the Repository**: 
   ```bash
   git clone https://github.com/Eswar1254/weather.git
