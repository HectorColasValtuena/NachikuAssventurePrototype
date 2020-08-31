import sys	#sys.argv
import os	#file management: os.path.isfile(), os.rename(), os.remove()
import yaml	#yaml

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
		print ("* $> py PySpriteRigger.py filePath1/fileName1 filePath2/fileName2")

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
			raise
		#move original file to backup location
		originalFilePath = moveToBackup (filePath);

		#then read it, process the contents, and re-write it to the original file path


	#return true if success, false if failed
	except:
		print ("! Failure")
		return False
	else:
		print ("> Success")
		return True

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