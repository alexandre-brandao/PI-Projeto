<?php

//Includes file with operations
require_once '../includes/DbOperations.php';
$response = array();

if($_SERVER['REQUEST_METHOD']=='POST'){

	if(
		//Required parameters
		isset($_POST['tag_code']) and
			isset($_POST['prototype_id'])
		){

		//Operation
		$db = new DbOperations();

		$result = $db->removePrototype(
									$_POST['tag_code'],
									$_POST['prototype_id']
									);

		if($result == 1){
			$response['eror'] = false;
			$response['message'] = "Prototype removed successfully";
		}elseif($result == 2){
			$response['error'] = true;
			$response['message'] = "An error occurred, please try again";
		}elseif($result == 0){
			$response['error'] = true;
			$response['message'] = "Tag Code/Prototype ID don't match any registered prototype.";
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