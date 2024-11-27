// Asegúrate de inicializar Firebase antes de usar estas funciones
firebaseAuth = {
    // Configura Firebase
    var firebaseConfig = {
        apiKey: "AIzaSyA_UONSAAM8OrTELLhXIL8DmyW-DNi45Ho",
        authDomain: "gestionnegocio-f17f0.firebaseapp.com",
        projectId: "gestionnegocio-f17f0",
        storageBucket: "gestionnegocio-f17f0.firebasestorage.app",
        messagingSenderId: "671733944070",
        appId: "1:671733944070:web:5458e5dba9523733995887",
        measurementId: "G-RFP074C17T"
    };
    // Inicializa Firebase
    if(!firebase.apps.length) {
    firebase.initializeApp(firebaseConfig);
} else {
    firebase.app(); // Usa la instancia de Firebase si ya está inicializada
}

window.firebaseAuth = {
    async registerWithEmailPassword(email, password) {
        console.log("Intentando registrar usuario..."); // Mensaje de depuración
        try {
            const userCredential = await firebase.auth().createUserWithEmailAndPassword(email, password);
            return userCredential.user.uid; // Retorna el ID de usuario
        } catch (error) {
            throw new Error(error.message); // Pasa el mensaje de error a .NET
        }
    },

    async signInWithEmailPassword(email, password) {
        const userCredential = await firebase.auth().signInWithEmailAndPassword(email, password);
        return userCredential.user.uid;
    },

    async signOut() {
        await firebase.auth().signOut();
    }
};
