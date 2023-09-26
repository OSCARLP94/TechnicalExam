// Importa las funciones necesarias desde los SDKs que necesitas
import { initializeApp } from "https://www.gstatic.com/firebasejs/10.4.0/firebase-app.js";
import { getAnalytics } from "https://www.gstatic.com/firebasejs/10.4.0/firebase-analytics.js";

// Tu configuración de Firebase
const firebaseConfig = {
    apiKey: "AIzaSyBa2cFUL5oGDCgCIlsKMnIiIhPVq3hAZvI",
    authDomain: "technicalexam-db7ca.firebaseapp.com",
    projectId: "technicalexam-db7ca",
    storageBucket: "technicalexam-db7ca.appspot.com",
    messagingSenderId: "198662306790",
    appId: "1:198662306790:web:1ce829ff1db55a83aab427",
    measurementId: "G-Q4YFVR5QDP"
};

// Inicializa Firebase
const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);

// Exporta las variables que necesitas usar en otros scripts si es necesario
export { app, analytics };