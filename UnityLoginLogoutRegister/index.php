<?php

include("config.php");

function registerUser($username, $password, $email, $sex) {
    global $connection;

    // Validar el correo electrónico
    if (!filter_var($email, FILTER_VALIDATE_EMAIL)) {
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
        $stmt = $connection->prepare("INSERT INTO users (username, email, password, sex) VALUES (:username, :email, :password, :sex)");
        $stmt->bindParam(':username', $username);
        $stmt->bindParam(':email', $email);
        $stmt->bindParam(':password', $hashedPassword);
        $stmt->bindParam(':sex', $sex);

        if ($stmt->execute()) {
            echo 1; // Registro exitoso
        } else {
            echo 2; // Error al registrar
        }
    } else {
        echo 3; // Usuario ya existe
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
            echo json_encode(['userId' => $user['user_id'], 'status' => 1]); // Inicio exitoso con ID de usuario
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

function getUserInfo($userId) {
    global $connection;

    $stmt = $connection->prepare("SELECT * FROM users WHERE user_id = :user_id");
    $stmt->bindParam(':user_id', $userId);
    $stmt->execute();

    $user = $stmt->fetch(PDO::FETCH_ASSOC);

    if ($user) {
        echo json_encode($user);
    } else {
        echo json_encode(['error' => 'User not found']);
    }
}

function updateUser($userId, $username, $password, $email, $sex) {
    global $connection;

    // Validar el correo electrónico
    if (!filter_var($email, FILTER_VALIDATE_EMAIL)) {
        echo 4;
        return;
    }

    // Verificar si el usuario existe
    $stmt = $connection->prepare("SELECT * FROM users WHERE user_id = :user_id");
    $stmt->bindParam(':user_id', $userId);
    $stmt->execute();

    if ($stmt->rowCount() > 0) {
        // Encriptar la contraseña
        $hashedPassword = password_hash($password, PASSWORD_DEFAULT);
        // Actualizar el usuario con la nueva información
        $stmt = $connection->prepare("UPDATE users SET username = :username, email = :email, password = :password, sex = :sex WHERE user_id = :user_id");
        $stmt->bindParam(':user_id', $userId);
        $stmt->bindParam(':username', $username);
        $stmt->bindParam(':email', $email);
        $stmt->bindParam(':password', $hashedPassword);
        $stmt->bindParam(':sex', $sex);

        if ($stmt->execute()) {
            echo 1; // Actualización exitosa
        } else {
            echo 2; // Error al actualizar
        }
    } else {
        echo 3; // Usuario no encontrado
    }
}

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    if (isset($_POST["username"]) && isset($_POST["password"]) && isset($_POST["email"]) && isset($_POST["sex"])) {
        $username = $_POST["username"];
        $password = $_POST["password"];
        $email = $_POST["email"];
        $sex = $_POST["sex"];
        if (!empty($username) && !empty($password) && !empty($email) && !empty($sex)) {
            registerUser($username, $password, $email, $sex);
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
    } elseif (isset($_POST["updateUserId"]) && isset($_POST["updateUsername"]) && isset($_POST["updatePassword"]) && isset($_POST["updateEmail"]) && isset($_POST["updateSex"])) {
        $userId = $_POST["updateUserId"];
        $username = $_POST["updateUsername"];
        $password = $_POST["updatePassword"];
        $email = $_POST["updateEmail"];
        $sex = $_POST["updateSex"];
        if (!empty($userId) && !empty($username) && !empty($password) && !empty($email) && !empty($sex)) {
            updateUser($userId, $username, $password, $email, $sex);
        } else {
            echo "Todos los campos son requeridos.";
        }
    }
} elseif ($_SERVER['REQUEST_METHOD'] === 'GET') {
    if (isset($_GET['user_id'])) {
        $userId = $_GET['user_id'];
        getUserInfo($userId);
    } else {
        echo json_encode(['error' => 'User ID not provided']);
    }
} else {
    echo "Método no permitido";
}
?>
