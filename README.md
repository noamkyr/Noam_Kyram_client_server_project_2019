# My Fantasy - Basketball Fantasy Game

**My Fantasy** is an Android app designed for basketball enthusiasts, offering a fantasy game based on real N.B.A. league players from specific seasons. This project demonstrates a blend of mobile development, data integration, and backend server management.

## Key Features:
- **Team Building:** The user can select a specific N.B.A. season, draft players by position (Guard, Forward, Center), and create their ideal lineup from the active players of that season.
- **Real-Time Statistics:** Player stats are fetched from the official N.B.A. site using REST API services.
- **Competitive Play:** Users can challenge friends to build their own teams and compete based on aggregated player statistics, including points, assists, rebounds, blocks, and steals.

## Technologies & Tools:
- **Android Development:** The app is built natively for Android using Java and XML for the user interface.
- **Backend Server:** A Python-based server, periodically scraping the N.B.A. API for the latest player stats and storing them at the server in JSON format.
- **Requests:** Used to interact with the N.B.A. REST API, ensuring accurate and up-to-date data.
- **REST API:** The server communicates with the client app via a custom REST API, managing user authentication, team creation, and real-time score updates.
- **Data Storage:** User data and team configurations are stored on the server for minimal local storage and user session management.
