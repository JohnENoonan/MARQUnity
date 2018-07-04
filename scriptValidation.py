import sys, json


if __name__ == "__main__":
	file_ = sys.argv[1]
	script = json.load(open(file_,'r'))
	script = script["Items"]
	for i in xrange(len(script) -1):
		if script[i]['type'] != "dialogue":
			if script[i+1]['type'] != "dialogue":
				print script[i+1]