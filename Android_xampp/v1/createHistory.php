<?php

//Includes file with operations
require_once '../includes/DbOperations.php';
$response = array();

if($_SERVER['REQUEST_METHOD']=='POST'){

	if(
		//Required parameters
		isset($_POST['tag_code']) and
			isset($_POST['location']) and
				isset($_POST['date'])
		){

		//Operation
		$db = new DbOperations();

		$result = $db->createHistory(
									$_POST['tag_code'],
									$_POST['location'],
									$_POST['date']
									);

		if($result == 1){
			$response['eror'] = false;
			$response['message'] = "History entry registered successfully";
		}elseif($result == 2){
			$response['error'] = true;
			$response['message'] = "An error occurred, please try again";
		}elseif($result == 0){
			$response['error'] = true;
			$response['message'] = "ID/Tag Code don't match any history entry.";
		}

	}else{
		$response['error'] = true;
		$response['message'] = "Required fields are missing";
	}

}else{
	$response['error'] = true;
	$response['message'] = "Invalid Request";
}

echo json_encode($response);
