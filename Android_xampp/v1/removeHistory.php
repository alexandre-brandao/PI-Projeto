<?php

//Includes file with operations
require_once '../includes/DbOperations.php';
$response = array();

if($_SERVER['REQUEST_METHOD']=='POST'){

	if(
		//Required parameters
		isset($_POST['id']) and
			isset($_POST['tag_code'])
		){

		//Operation
		$db = new DbOperations();

		$result = $db->removeHistory(
									$_POST['id'],
									$_POST['tag_code']
									);

		if($result == 1){
			$response['eror'] = false;
			$response['message'] = "History entry removed successfully";
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