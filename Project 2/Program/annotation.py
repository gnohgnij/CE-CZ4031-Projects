"""
contains code for generating the annotations
"""
import json
from tkinter.constants import BOTTOM, E, SUNKEN, X
import PySimpleGUI as sg
import preprocessing
import tkinter as tk
import Pmw

#
# GLOBAL VARIABLES
# Drawing Query Plan Starts Here
#

RECT_WIDTH = 200
RECT_HEIGHT = 60
CANVAS_WIDTH = 1000
CANVAS_HEIGHT = 1000

all_operators = []
visual_to_node = {}
instance = None
MAX_DURATION=0

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
    # plan = json.load(json_plan)
    plan = json_plan
    curr_op.operation = plan["Node Type"]
    curr_op.information = get_current_operator_info(plan)

    all_operators.append(curr_op)

    #check if they are child plans
    if "Plans" in plan:
        children_num = len(plan["Plans"])
        for i in range(children_num):
            x1 = (i) * ((curr_op.x2 - curr_op.x1) / children_num)
            x2 = (i + 1) * ((curr_op.x2 - curr_op.x1) / children_num)
            y1 = curr_op.y2 + RECT_WIDTH
            y2 = curr_op.y2 + 2 * RECT_HEIGHT

            child_op = Operator(x1, x2, y1, y2, "", "")
            curr_op.add_child(child_op)
            build_plan(child_op, plan["Plans"][i])

def get_current_operator_info(operator):

    data = operator
    node_type = data['Node Type']
    duration = "\nDuration: " + str(data['Actual Total Time'] - data['Actual Startup Time']) + " ms"
    
    if node_type == 'Bitmap Heap Scan':
        info = node_type + ' operator on '
        info += data['Relation Name']
        
    elif node_type == 'Bitmap Index Scan':
        info = node_type +' operator on '
        info += data['Index Name']
    
    elif node_type == 'BitmapAnd':
        info = node_type + ' operator'
        info += duration
    
    elif node_type == 'BitmapOr':
        info = node_type + ' operator'
        info += duration
    
    elif node_type == 'Aggregate':
        info = node_type + ' operator'
        info += duration
    
    elif node_type == 'Gather':
        info = node_type + ' operator'
        info += duration

    elif node_type == 'Seq Scan':
        info = node_type + ' operator'
        # info += data['Relation Name'] + '\n'
        # info += 'Filter on ' + data['Filter'] + '\n'
        info += duration
    
    elif node_type == 'Gather Merge':
        info = node_type + ' operator'
        info += duration
    
    elif node_type == 'Sort':
        info = node_type + ' operator'
        info += duration

    elif node_type == 'Limit':
        info = node_type + ' operator'
        info += duration

    elif node_type == 'Nested Loop':
        info = node_type + ' operator'
        info += duration

    elif node_type == 'Hash Join':
        info = node_type + ' operator'
        info += duration
    
    elif node_type == 'Merge Join':
        info = node_type + ' operator'
        info += duration

    elif node_type == 'Merge Append':
        info = node_type + ' operator'
        info += duration

    elif node_type == 'Hash':
        info = node_type + ' operator'
        info += duration
    
    elif node_type == 'HashAggregate':
        info = node_type + ' operator'
        info += duration
    
    elif node_type == 'HashSetOp':
        info = node_type + ' operator'
        info += duration

    elif node_type == 'Index Scan':
        info = node_type + ' operator on'
        info += data['Index Name']
    
    elif node_type == 'Append':
        info = node_type + ' operator'
        info += duration
    
    elif node_type == 'CTE Scan':
        info = node_type + ' operator'
        info += duration
    
    elif node_type == 'Function Scan':
        info = node_type + ' operator'
        info += duration
    
    elif node_type == 'Group':
        info = node_type + ' operator'
        info += duration
    
    elif node_type == 'GroupAggregate':
        info = node_type + ' operator'
        info += duration
    
    elif node_type == 'Incremental Sort':
        info = node_type + ' operator'
        info += duration
    
    elif node_type == 'Materialize':
        info = node_type + ' operator'
        info += duration

    elif node_type == 'ModifyTable':
        info = node_type + ' operator'
        info += duration
    
    elif node_type == 'Recursive Union':
        info = node_type + ' operator'
        info += duration

    elif node_type == 'Result':
        info = node_type + ' operator'
        info += duration

    elif node_type == 'SetOp':
        info = node_type + ' operator'
        info += duration
    
    elif node_type == 'Subquery Scan':
        info = node_type + ' operator'
        info += duration

    elif node_type == 'TID Scan':
        info = node_type + ' operator'
        info += duration
    
    elif node_type == 'Unique':
        info = node_type + ' operator'
        info += duration
    
    elif node_type == 'Values Scan':
        info = node_type + ' operator'
        info += duration
    
    elif node_type == 'WorkTable Scan':
        info = node_type + ' operator'
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

    root_op = Operator(0, CANVAS_WIDTH,10, RECT_HEIGHT, "", "")
    build_plan(root_op, data)

    root = tk.Tk()
    root.state("zoomed")
    root.title("CX4031 Project 2 GUI")
    frame = tk.Frame(root, width = 1000, height = 1200, bg="yellow")
    frame.pack(expand=True, fill="both")

    def close():
        root.quit()

    button = tk.Button(frame, text = 'Close the window', command = close)
    button.place(relx=0, rely=0)

    #query plan canvas
    canvas = tk.Canvas(frame, bg='red', scrollregion=(0, 0, 1000, 600))
    scrollbar = tk.Scrollbar(frame, orient = tk.VERTICAL)
    scrollbar.place(relx=0.99, rely=0, relheight=0.5, relwidth=0.01)
    scrollbar.config(command=canvas.yview)
    canvas.config(yscrollcommand=scrollbar.set)
    canvas.place(relx=0, rely=0, relheight=0.5, relwidth=1)
    
    #query text
    query_text_canvas = tk.Canvas(frame, bg = 'blue', scrollregion=(0, 0, 500, 600))

    query_text_scrollbar_v = tk.Scrollbar(frame, orient = tk.VERTICAL)
    query_text_scrollbar_v.place(relx=0.49, rely=0.5, relwidth=0.01, relheight=0.5)
    query_text_scrollbar_v.config(command=query_text_canvas.yview)

    query_text_scrollbar_h = tk.Scrollbar(frame, orient = tk.HORIZONTAL)
    query_text_scrollbar_h.place(relx=0, rely=0.98, relwidth=0.5, relheight=0.02)
    query_text_scrollbar_h.config(command=query_text_canvas.xview)

    query_text_canvas.config(yscrollcommand = query_text_scrollbar_v.set, xscrollcommand = query_text_scrollbar_h.set)
    query_text_canvas.place(relx=0, rely=0.5, relheight=0.48, relwidth=0.49)
    query_text_canvas.create_text(250, 70, text=pretty_query_text)

    #query json
    query_json_canvas = tk.Canvas(frame, bg = 'blue', scrollregion=(0, 0, 500, 600))
    query_json_scrollbar_v = tk.Scrollbar(frame, orient = tk.VERTICAL)
    query_json_scrollbar_v.place(relx=0.99, rely=0.5, relwidth=0.01, relheight=0.5)
    query_json_scrollbar_v.config(command=query_json_canvas.yview)

    query_json_scrollbar_h = tk.Scrollbar(frame, orient = tk.HORIZONTAL)
    query_json_scrollbar_h.place(relx=0.5, rely=0.98, relwidth=0.5, relheight=0.02)
    query_json_scrollbar_h.config(command=query_json_canvas.xview)

    query_json_canvas.config(xscrollcommand=query_json_scrollbar_h.set, yscrollcommand=query_json_scrollbar_v.set)
    query_json_canvas.place(relx=0.5, rely=0.5, relheight=0.48, relwidth=0.49)
    query_json_canvas.create_text(250, 70, text = query_plan)

    tk.Misc.lift(button)

    number = 1
    test = query_plan.replace('"Plans": [', '').split("{")

    # 3 different for loops are needed for logical binding of rectangles in the node_list
    for element in all_operators:
        x = element.center[0]
        y = element.center[1]
        rect = canvas.create_rectangle(x - RECT_WIDTH / 2, y + RECT_HEIGHT / 2, x + RECT_WIDTH / 2, y - RECT_HEIGHT / 2,
                                    fill='grey')
        balloon = Pmw.Balloon()
        balloon.tagbind(canvas, rect, test[number].replace('  ', '').replace('}\n]','').replace('}',''))
        visual_to_node[rect] = element
        number+=1

    for element in all_operators:
        gui_text = canvas.create_text((element.center[0], element.center[1]), text=element.information, tags="clicked")
        visual_to_node[gui_text] = element

    for element in all_operators:
        for child in element.children:
            canvas.create_line(child.center[0], child.center[1] - RECT_HEIGHT / 2, element.center[0],
                            element.center[1] + RECT_HEIGHT / 2, arrow = tk.LAST) 
    
    root.mainloop()

def enter(event,canvas):
    node = visual_to_node[event.widget.find_withtag("current")[0]]
    global instance
    if node.duration == MAX_DURATION:
        instance = canvas.create_text(200, 50, text=node.information,
                                      fill="red", width=350)
    else:
        instance = canvas.create_text(200, 50, text=node.information,
                                      fill="blue", width=350)

def leave(event,canvas):
    canvas.delete(instance)



if __name__ == '__main__':

    draw("query_plan.json")

