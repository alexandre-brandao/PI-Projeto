<?php

//Includes file with operations
require_once '../includes/DbOperations.php';
$response = array();

if($_SERVER['REQUEST_METHOD']=='POST'){

	if(isset($_POST['email']) and isset($_POST['password'])){

		$db = new DbOperations();

		if($db->userLogin($_POST['email'], $_POST['password'])){
			$user = $db->getUserByEmail($_POST['email']);
			$response['error'] = false;
			$response['name'] = $user['name'];
			$response['email'] = $user['email'];
			$response['phone'] = $user['phone'];
			$response['access'] = $user['access'];
		}else{
			$response['error'] = true;
			$response['message'] = "Invalid email or password";
		}

	}else{
		$response['error'] = true;
		$response['message'] = "Required fields are missing";
	}

}

echo json_encode($response);