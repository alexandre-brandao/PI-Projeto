<?php

//Includes file with operations
require_once '../includes/DbOperations.php';
$response = array();

if($_SERVER['REQUEST_METHOD']=='POST'){

	if(
		//Required parameters
		isset($_POST['tag_code'])
		){

		//Operation
		$db = new DbOperations();

		$numrows = $db->searchHistory($_POST['tag_code']);

		if($numrows>0){

			for($i = 0; $i < $numrows; $i++){
				$response[$i] = array();
				$user = $db->getHistoryByTag($_POST['tag_code']);
				$response[$i]['error'] = false;
				$response[$i]['tag_code'] = $user[$i]['tag_code'];
				$response[$i]['location'] = $user[$i]['location'];
				$response[$i]['date'] = $user[$i]['date'];
			}

		}else{
			$response['error'] = true;
			$response['message'] = "ID/Tag Code don't match any history entry.";
		}
	
	}else{
		$response['error'] = true;
		$response['message'] = "Required fields are missing";
	}
}

echo json_encode($response);