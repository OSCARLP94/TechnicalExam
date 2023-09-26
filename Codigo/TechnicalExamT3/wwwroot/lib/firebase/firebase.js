import { initializeApp } from "https://www.gstatic.com/firebasejs/10.4.0/firebase-app.js";
import { getAnalytics } from "https://www.gstatic.com/firebasejs/10.4.0/firebase-analytics.js";

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
    apiKey: "AIzaSyBa2cFUL5oGDCgCIlsKMnIiIhPVq3hAZvI",
    authDomain: "technicalexam-db7ca.firebaseapp.com",
    projectId: "technicalexam-db7ca",
    storageBucket: "technicalexam-db7ca.appspot.com",
    messagingSenderId: "198662306790",
    appId: "1:198662306790:web:1ce829ff1db55a83aab427",
    measurementId: "G-Q4YFVR5QDP"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);

function registerEvent(typeAction) {
    analytics.logEvent('user_log', {
        typeAction: typeAction
    });

    $("#DeleteForm").submit();
}

// Adjunta manejadores de eventos usando jQuery
$(document).ready(function () {
    $("#btnDelete").on("click", function () {
        var actionName = "@Model.Action.ActionName";
        registerEvent(actionName);
    });
});