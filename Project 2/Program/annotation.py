"""
contains code for generating the annotations
"""
import json
import tkinter as tk
import Pmw

#
# GLOBAL VARIABLES
#

RECT_WIDTH = 200
RECT_HEIGHT = 60
CANVAS_WIDTH = 1000
CANVAS_HEIGHT = 1000

all_operators = []
visual_to_node = {}

# Information of rectangle
class Operator:
    def __init__(self, x1, x2, y1, y2, operation, information):
        # four corners coordinates
        self.x1 = x1
        self.x2 = x2
        self.y1 = y1
        self.y2 = y2
        self.operation = operation
        self.information = information
        self.center = ((x1+x2)/2,(y1+y2)/2)
        self.children = []

    def add_child(self, child):
        self.children.append(child)

def build_plan(curr_op, json_plan):
    plan = json_plan
    curr_op.operation = plan["Node Type"]
    curr_op.information = get_current_operator_info(plan)

    all_operators.append(curr_op)

    #   (x1, y1) ======================== (x2, y1)
    #            |                      |
    #            |                      |
    #            |                      |
    #   (x1, y2) ======================== (x2, y2)

    #check if there are child plans
    if "Plans" in plan:
        children_num = len(plan["Plans"])
        if(children_num == 1):
            for i in range(children_num):
                x1 = curr_op.x1
                x2 = curr_op.x2
                y1 = curr_op.y2 + RECT_HEIGHT/2
                y2 = y1 + RECT_HEIGHT

                child_op = Operator(x1, x2, y1, y2, "", "")
                curr_op.add_child(child_op)
                build_plan(child_op, plan["Plans"][i])

        elif (children_num == 2):
            for i in range(children_num):
                x2 = curr_op.x1 - RECT_WIDTH + i*(4*RECT_WIDTH)
                x1 = x2 - RECT_WIDTH
                y1 = curr_op.y2 + RECT_HEIGHT
                y2 = y1 + RECT_HEIGHT
                
                child_op = Operator(x1, x2, y1, y2, "", "")
                curr_op.add_child(child_op)
                build_plan(child_op, plan["Plans"][i])

                

def get_current_operator_info(operator):

    data = operator
    node_type = data['Node Type']
    duration = "\nDuration: " + str(data['Actual Total Time'] - data['Actual Startup Time']) + " ms"
    
    if node_type == 'Bitmap Heap Scan':
        info = 'Peform ' + node_type +  ', on table ' + data['Relation Name'] + ' with filter ' + data['Filter'].replace("AND", "AND \n")
        info += duration
        
    elif node_type == 'Bitmap Index Scan':
        info = 'Peform ' + node_type + ', on index ' + data['Index Name'] + ' with index condition ' + data['Index Cond']
        info += duration
    
    elif node_type == 'BitmapAnd':
        info = 'Peform ' + node_type
        info += duration
    
    elif node_type == 'BitmapOr':
        info = 'Peform ' + node_type
        info += duration
    
    elif node_type == 'Aggregate':
        info = 'Perform ' + node_type
        if 'Group Key' in data:
                info += ', with grouping on attribute(s) ' + ''.join(str(e) + ", " for e in data['Group Key'])
        if 'Filter' in data:
                info += ', with filter on ' + data['Filter'].replace("AND", "AND \n")
        if 'Hash Key' in data:
            info += ', with hashing on attribute(s) ' + ''.join(str(e) + ", " for e in data['Hash Key'])
        info += duration
    
    elif node_type == 'Gather':
        info = 'Perform ' + node_type
        info += duration

    elif node_type == 'Seq Scan':
        info = 'Perform ' + node_type + ', on relation ' + data['Relation Name']
        if 'Filter' in data:
            info += ', with filter ' + data['Filter'].replace("AND", "AND \n")
        info += duration
    
    elif node_type == 'Gather Merge':
        info = 'Perform ' + node_type
        info += duration
    
    elif node_type == 'Sort':
        info = 'Perform ' + node_type + ', on attribute(s) ' + ''.join(str(e) + ", " for e in data['Sort Key']) + ' using ' + data['Sort Method']
        info += duration

    elif node_type == 'Limit':
        info = 'Perform ' + node_type
        info += duration

    elif node_type == 'Nested Loop':
        info = 'Perform ' + node_type + ', using join type ' + data['Join Type']
        info += duration

    elif node_type == 'Hash Join':
        info = 'Perform ' + node_type + ', using join type ' + data['Join Type'] + ', with hash condition ' + data['Hash Cond']
        info += duration
    
    elif node_type == 'Merge Join':
        info = 'Perform ' + node_type + ', using join type ' + data['Join Type'] + ', with merge condition ' + data['Merge Cond']
        info += duration

    elif node_type == 'Merge Append':
        info = 'Perform ' + node_type + ', on attribute(s) ' + ''.join(str(e) + ", " for e in data['Sort Key'])
        info += duration

    elif node_type == 'Hash':
        info = 'Perform ' + node_type
        info += duration
    
    elif node_type == 'HashAggregate':
        info = 'Perform ' + node_type
        if 'Group Key' in data:
            info += ', with grouping on ' + ''.join(str(e) + ", " for e in data['Group Key'])
        if 'Hash Key' in data:
            info += ', with grouping on ' + ''.join(str(e) + " " for e in data['Hash Key'])
        info += duration
    
    elif node_type == 'HashSetOp':
        info = 'Perform ' + node_type
        info += duration

    elif node_type == 'Index Scan':
        info = 'Perform ' + node_type + ', on index ' + data['Index Name'] + ', of relation ' + data['Relation Name'] + ', with index condition ' + data['Index Cond']
        info += duration
    
    elif node_type == 'Append':
        info = 'Perform ' + node_type
        info += duration
    
    elif node_type == 'CTE Scan':
        info = 'Perform ' + node_type + ', with filter on ' + data['Filter'].replace("AND", "AND \n")
        info += duration
    
    elif node_type == 'Function Scan':
        info = 'Perform ' + node_type + ', with filter on ' + data['Filter'].replace("AND", "AND \n")
        info += duration
    
    elif node_type == 'Group':
        info = 'Perform ' + node_type
        info += duration
    
    elif node_type == 'GroupAggregate':
        info = 'Perform ' + node_type
        info += duration
    
    elif node_type == 'Incremental Sort':
        info = 'Perform ' + node_type + ', on attribute(s) ' + ''.join(str(e) + ", " for e in data['Sort Key']) + ' using sort method ' + data['Sort Method']
        info += duration
    
    elif node_type == 'Materialize':
        info = 'Perform ' + node_type
        info += duration

    elif node_type == 'ModifyTable':
        info = 'Perform ' + node_type + ', on relation ' + data['Relation Name'] 
        info += duration
    
    elif node_type == 'Recursive Union':
        info = 'Perform ' + node_type
        info += duration

    elif node_type == 'Result':
        info = 'Perform ' + node_type
        info += duration

    elif node_type == 'SetOp':
        info = 'Perform ' + node_type
        info += duration
    
    elif node_type == 'Subquery Scan':
        info = 'Perform ' + node_type + ', with filter ' + data['Filter'].replace("AND", "AND \n")
        info += duration

    elif node_type == 'TID Scan':
        info = 'Perform ' + node_type + ', on relation ' + data['Relation Name']
        if 'Tid Cond' in data:
            info += ' with TID Cond ' + data['Tid Cond']
        info += duration
    
    elif node_type == 'Unique':
        info = 'Perform ' + node_type
        info += duration
    
    elif node_type == 'Values Scan':
        info = 'Perform ' + node_type
        info += duration
    
    elif node_type == 'WorkTable Scan':
        info = 'Perform ' + node_type + ', with filter ' + data['Filter'].replace("AND", "AND \n")
        info += duration

    return info

def draw(query_plan, query): 

    #make query text look nice
    pretty_query_text = ''
    for text in query.lower().split(' '):
        if text == 'from' or  text == "where":
            pretty_query_text += '\n' + text + " "
        elif text == 'and' or  text == "or":
            pretty_query_text += '\n\t' + text + " "
        else:
            pretty_query_text += text + " "

    data = json.loads(query_plan)
    all_operators.clear()

    root_op = Operator(500, 500+RECT_WIDTH, 0, 0+RECT_HEIGHT, "", "")    #CANVAS_WIDTH = 1000, RECT_HEIGHT = 60, CENTER = (500, 35)
    build_plan(root_op, data)

    root = tk.Tk()
    root.state("zoomed")
    root.title("CX4031 Project 2 GUI")
    frame = tk.Frame(root, width = 1000, height = 1200)
    frame.pack(expand=True, fill="both")

    def close():
        root.quit()

    button = tk.Button(frame, text = 'Close the window', command = close)
    button.place(relx=0, rely=0)

    #query plan canvas
    canvas = tk.Canvas(frame, bg='white')

    scrollbar_v = tk.Scrollbar(frame, orient = tk.VERTICAL)
    scrollbar_v.place(relx=0.99, rely=0, relheight=0.5, relwidth=0.01)
    scrollbar_v.config(command=canvas.yview)

    scrollbar_h = tk.Scrollbar(frame, orient = tk.HORIZONTAL)
    scrollbar_h.place(relx=0, rely=0.48, relheight=0.02, relwidth=0.99)
    scrollbar_h.config(command=canvas.xview)

    canvas.config(yscrollcommand=scrollbar_v.set, xscrollcommand=scrollbar_h.set)
    canvas.bind('<Configure>', lambda e: canvas.configure(scrollregion=canvas.bbox("all")))
    canvas.place(relx=0, rely=0, relheight=0.48, relwidth=0.99)
    
    #query text
    query_text_canvas = tk.Canvas(frame, bg = 'white')

    query_text_scrollbar_v = tk.Scrollbar(frame, orient = tk.VERTICAL)
    query_text_scrollbar_v.place(relx=0.49, rely=0.5, relwidth=0.01, relheight=0.5)
    query_text_scrollbar_v.config(command=query_text_canvas.yview)

    query_text_scrollbar_h = tk.Scrollbar(frame, orient = tk.HORIZONTAL)
    query_text_scrollbar_h.place(relx=0, rely=0.98, relwidth=0.5, relheight=0.02)
    query_text_scrollbar_h.config(command=query_text_canvas.xview)

    query_text_canvas.config(yscrollcommand = query_text_scrollbar_v.set, xscrollcommand = query_text_scrollbar_h.set)
    query_text_canvas.bind('<Configure>', lambda e: query_text_canvas.configure(scrollregion=query_text_canvas.bbox("all")))
    query_text_canvas.place(relx=0, rely=0.5, relheight=0.48, relwidth=0.49)
    query_text_canvas.create_text(250, 70, text=pretty_query_text)

    #query json
    query_json_canvas = tk.Canvas(frame, bg = 'white')
    query_json_scrollbar_v = tk.Scrollbar(frame, orient = tk.VERTICAL)
    query_json_scrollbar_v.place(relx=0.99, rely=0.5, relwidth=0.01, relheight=0.5)
    query_json_scrollbar_v.config(command=query_json_canvas.yview)

    query_json_scrollbar_h = tk.Scrollbar(frame, orient = tk.HORIZONTAL)
    query_json_scrollbar_h.place(relx=0.5, rely=0.98, relwidth=0.5, relheight=0.02)
    query_json_scrollbar_h.config(command=query_json_canvas.xview)

    query_json_canvas.config(xscrollcommand=query_json_scrollbar_h.set, yscrollcommand=query_json_scrollbar_v.set)
    query_json_canvas.bind('<Configure>', lambda e: query_json_canvas.configure(scrollregion=query_json_canvas.bbox("all")))
    query_json_canvas.place(relx=0.5, rely=0.5, relheight=0.48, relwidth=0.49)
    query_json_canvas.create_text(250, 70, text = query_plan)

    tk.Misc.lift(button)

    # store unique coordinates
    unique_coordinates = []
    for element in all_operators:
        coordinates = (element.x1, element.x2, element.y1, element.y2)
        if coordinates not in unique_coordinates:
            unique_coordinates.append(coordinates)
        else:
            element.x1 += 3*RECT_WIDTH/2
            element.x2 += 3*RECT_WIDTH/2
            element.center = ((element.x1+element.x2)/2,(element.y1+element.y2)/2)
            new_coor = (element.x1, element.x2, element.y1, element.y2)
            unique_coordinates.append(new_coor)

    # create rectangles
    for element in all_operators:
        x1 = element.x1
        x2 = element.x2
        y1 = element.y1
        y2 = element.y2
        rect = canvas.create_rectangle(x1, y1, x2, y2, fill="grey")

        # create tooltip
        balloon = Pmw.Balloon()
        balloon.tagbind(canvas, rect, element.information)
        visual_to_node[rect] = element

    # create text on rectangles
    for element in all_operators:
        gui_text = canvas.create_text((element.center[0], element.center[1]), text=element.operation)
        visual_to_node[gui_text] = element

    # create arrows
    for element in all_operators:
        for child in element.children:
            canvas.create_line(child.center[0], child.center[1] - RECT_HEIGHT/2, element.center[0],
                            element.center[1] + RECT_HEIGHT/2, arrow = tk.LAST) 
    
    root.mainloop()

if __name__ == '__main__':

    draw("query_plan.json")

