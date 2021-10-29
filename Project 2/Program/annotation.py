"""
contains code for generating the annotations
"""
import json

def parse_query_plan(plan):
    data = json.loads(plan)
    node_type = data['Node Type']

    if node_type == 'Bitmap Heap Scan':
        text = node_type + ' operator on '
        text += data['Relation Name']
        print(text)

    elif node_type == 'Bitmap Index Scan':
        text = node_type +' operator on '
        text += data['Index Name']
        print(text)
    
    elif node_type == 'Aggregate':
        text = node_type + ' operator'
        if data['Strategy'] == 'Plain':
            text += "\n" + str(data['Actual Total Time'] - data['Actual Startup Time'])
            print(text)
    
    elif node_type == 'Gather':
        text = node_type + ' operator\n'
        text += str(data['Actual Total Time'] - data['Actual Startup Time'])
        print(text)

    elif node_type == 'Seq Scan':
        text = node_type + ' operator on '
        text += data['Relation Name'] + '\n'
        text += 'Filter on ' + data['Filter'] + '\n'
        text += 'Duration: ' + str(data['Actual Total Time'] - data['Actual Startup Time'])
        print(text)

    try:
        plans = data['Plans']
        for i in range(len(plans)):
            parse_query_plan(json.dumps(plans[i]))
    except Exception:
        print("End of query plan")
