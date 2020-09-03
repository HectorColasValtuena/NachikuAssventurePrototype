import sys	#sys.argv
import os	#file management: os.path.isfile(), os.rename(), os.remove()
import yaml	#yaml

DEFAULT_BONE_NAME = "bone_"

verboseMode = True

def main ():
	#separate the parameters from the executed command
	parameterList = sys.argv[1:]
	#print greet, offering help if no parameters received
	printGreeting (len(sys.argv) <= 1)
	
	#execute full operation of each received path
	successCount = 0
	errorCount = 0
	for parameter in parameterList:
		if (processFile (parameter)):
			successCount += 1
		else:
			errorCount += 1
	#Print a results report
	printFinalReport (successCount, errorCount)

#Prints on-launch message
def printGreeting (printHelp):
	print ("================================")
	print ("*        PySpriteRigger        *")
	if (printHelp):
		print ("* Processes UnityEngine's sprite meta files, converting mesh data into bones")
		print ("* Usage: Pass the paths of target files as arguments")
		print ("* $> py PySpriteRigger.py filePath1/fileName1 filePath2/fileName2 ...")

#prints exit message and waits for any input to continue
def printFinalReport (successCount, errorCount):
	print ("* Successfully processed " + str(successCount) + " files")
	if (errorCount > 0):
		print ("!\n!!!! Number of files failed: " + str(errorCount) + "\n!")
	print ("Press ENTER to continue...")
	input ()

#processes an individual file
def processFile (filePath):
	try:
		print ("* Processing \"" + filePath + "\"");
		#check if file exists
		if (not os.path.isfile(filePath)):
			print ("! Nonexistent file")
			raise FileNotFoundError

		#move original file to backup location
		originalFilePath = moveToBackup (filePath)

		#read the file
		with open(originalFilePath) as file:
			fileContents = yaml.full_load(file)
			print ("  File read")
			#print (fileContents['TextureImporter']['spriteSheet']['vertices'])
			
			#process the contents
			fileContents['TextureImporter']['spriteSheet']['bones'] = boneListFromVertexList(fileContents['TextureImporter']['spriteSheet']['vertices'])
			fileContents['TextureImporter']['spriteSheet']['weights'] = weightListFromVertexList(fileContents['TextureImporter']['spriteSheet']['vertices'])

		#re-write the yaml to the original file path
			with open(filePath, 'w') as file:
				yaml.dump(fileContents, file)
				print ("  File written")

	#return true if success, false if failed
	except:
		print ("! Failure")
		print ("  ", sys.exc_info()[0])
		print ("  ", sys.exc_info()[1])
		return False
	else:
		print ("> Success")
		return True

#Process a vertex into a list of bones
#Creates a bone for each vertex at the vertex position
def boneListFromVertexList (vertexList):
	boneList = []
	for index, vertex in enumerate(vertexList):
	#for index in range(len(vertexList)):
		boneList.append(boneFromVertex(vertexList[index], index))
	return boneList

#Creates information for a single bone
def boneFromVertex (vertex, index):
	global DEFAULT_BONE_NAME
	return {
		'name'		: DEFAULT_BONE_NAME + str(index),
		'position'	: {'x': vertex['x'], 'y': vertex['y'], 'z': 0},
		'rotation'	: {'x': 0, 'y': 0, 'z': 0, 'w': 1},
		'length'	: 0,
		'parentId'	: -1
	}

#Creates a list of blendweights for every vertex
def weightListFromVertexList (vertexList):
	weightList = []
	for index, vertex in enumerate(vertexList):
		weightList.append(weightForSingleBone(index))
	return weightList

#Creates a blend weight linking a vertex and a single bone
def weightForSingleBone (boneIndex):
	return {
		'weight[0]': 1,
		'weight[1]': 0,
		'weight[2]': 0,
		'weight[3]': 0,
		'boneIndex[0]': boneIndex,
		'boneIndex[1]': 0,
		'boneIndex[2]': 0,
		'boneIndex[3]': 0
	}

#moves target file to its final backup location
def moveToBackup (filePath):
	backupPath = pathToBackupPath(filePath)
	#remove previous backup if existent
	if (os.path.isfile(backupPath)):
		os.remove(backupPath)
	#rename file and return new name
	os.rename(filePath, backupPath)
	return backupPath

#transforms a filepath into its backup's corresponding name path
def pathToBackupPath (filePath):
	return filePath + ".backup"

#initiate execution of main function
main()