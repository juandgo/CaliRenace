<?php

include("config.php");

function registerUser($username, $password, $email) {
    global $connection;
    
    // Validar el correo electrónico
    if (!filter_var($email, FILTER_VALIDATE_EMAIL)) {
        // echo "Correo electrónico no válido.";
        echo 4;
        return;
    }

    // Verificar si el nombre de usuario no está tomado
    $stmt = $connection->prepare("SELECT * FROM users WHERE username = :username");
    $stmt->bindParam(':username', $username);
    $stmt->execute();

    if ($stmt->rowCount() == 0) {
        // Encriptar la contraseña
        $hashedPassword = password_hash($password, PASSWORD_DEFAULT);
        // Insertar el usuario con la contraseña encriptada
        $stmt = $connection->prepare("INSERT INTO users (username, email, password) VALUES (:username, :email, :password)");
        $stmt->bindParam(':username', $username);
        $stmt->bindParam(':email', $email);
        $stmt->bindParam(':password', $hashedPassword);

        if ($stmt->execute()) {
            // echo "Usuario " . $username . " registrado exitosamente.";
            echo 1;
        } else {
            // echo "Error al registrar la cuenta.";
            echo 2;
        }
    } else {
        // echo "Este usuario no está disponible. Por favor cree otro usuario.";
        echo 3;
    }
}

function loginUser($username, $password) {
    global $connection;
    // Buscar el usuario en la base de datos
    $stmt = $connection->prepare("SELECT * FROM users WHERE username = :username");
    $stmt->bindParam(':username', $username);
    $stmt->execute();

    if ($stmt->rowCount() > 0) {
        $user = $stmt->fetch(PDO::FETCH_ASSOC);
        // Verificar la contraseña encriptada
        if (password_verify($password, $user['password'])) {
            echo 1; // Inicio exitoso
            session_start();
            $_SESSION['username'] = $username;
        } else {
            echo 2; // Contraseña incorrecta
        }
    } else {
        echo 3; // Usuario no encontrado
    }
}

function logoutUser() {
    session_start();
    session_unset();
    session_destroy();
    echo "Sesión cerrada";
}

function getUserInfo($username) {
    global $connection;

    $stmt = $connection->prepare("SELECT * FROM users WHERE username = :username");
    $stmt->bindParam(':username', $username);
    $stmt->execute();

    $user = $stmt->fetch(PDO::FETCH_ASSOC);

    if ($user) {
        echo json_encode($user);
    } else {
        echo json_encode(['error' => 'User not found']);
    }
}

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    if (isset($_POST["username"]) && isset($_POST["password"]) && isset($_POST["email"])) {
        $username = $_POST["username"];
        $password = $_POST["password"];
        $email = $_POST["email"];
        if (!empty($username) && !empty($password) && !empty($email)) {
            registerUser($username, $password, $email);
        } else {
            echo "Todos los campos son requeridos.";
        }
    } elseif (isset($_POST["loginUsername"]) && isset($_POST["loginPassword"])) {
        $username = $_POST["loginUsername"];
        $password = $_POST["loginPassword"];
        if (!empty($username) && !empty($password)) {
            loginUser($username, $password);
        } else {
            echo "Ambos campos son requeridos.";
        }
    } elseif (isset($_POST["logout"])) {
        logoutUser();
    }
} elseif ($_SERVER['REQUEST_METHOD'] === 'GET') {
    if (isset($_GET['username'])) {
        $username = $_GET['username'];
        getUserInfo($username);
    } else {
        echo json_encode(['error' => 'Username not provided']);
    }
} else {
    echo "Método no permitido";
}
?>
