# FilmLog – Personal Movie Tracking Mobile Application

A cross-platform mobile application built for the University of Pretoria's INF 354 module. FilmLog is a movie discovery and personal tracking platform that allows users to search for real movies, manage a personal Watchlist, and track movies they have watched — modelled after real-world apps like Letterboxd and IMDb.

---

## 🚀 Tech Stack

| Layer | Technology |
|---|---|
| Framework | Ionic 7 + Angular 17 |
| Language | TypeScript, HTML, SCSS |
| Local Storage | Ionic Storage (Angular) |
| External API | IMDb Unofficial Search API |
| Architecture | Component-based, Service layer pattern |

---

## ✨ Features

- User registration and login (simulated with local storage)
- Live movie search via external IMDb API
- Add movies to a personal Watchlist
- Mark movies as Watched and track rewatch count
- Reset watch count and move movies back to Watchlist
- Remove movies from Watchlist or Watched List
- Responsive UI across mobile and desktop screen sizes
- Classy dark theme with custom pink colour scheme

---

## ⚙️ Getting Started

### Prerequisites
- [Node.js](https://nodejs.org/) (v18+)
- [Ionic CLI](https://ionicframework.com/docs/cli) (`npm install -g @ionic/cli`)

### Setup & Run
```bash
# Clone the repository
git clone https://github.com/yourusername/filmlog.git
cd filmlog

# Install dependencies
npm install

# Run in browser
ionic serve
```

App will run on `http://localhost:8100`

> **Note:** Do not upload the `node_modules` folder. Run `npm install` after cloning.

---

## 🧠 Key Learning Outcomes

- Built and structured a multi-page Ionic Angular mobile application
- Integrated a live external REST API using Angular's `HttpClient` service
- Managed user-specific local data using Ionic Storage
- Implemented tab-based navigation and programmatic routing with Angular Router
- Applied responsive SCSS styling for both mobile and desktop viewports

---

## 📚 Module Context
Built as part of **INF 354 – Internet Programming** at the University of Pretoria (2026).

---

## 👩🏽‍💻 Author
**Maria Malebo Maleka** — [LinkedIn](https://linkedin.com/in/maria-malebo-maleka-5a405635b)
