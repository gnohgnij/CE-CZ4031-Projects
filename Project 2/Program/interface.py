"""
contains code for GUI
"""

import PySimpleGUI as sg

sg.theme('LightGrey1')   #theme of GUI

font = 'Consolas'

# All the stuff inside the window.
layout = [  [sg.Text('Query Execution Plan Annotator', key='-text-', font=(font, 20))],
            [sg.Text('Choose database to query', font=(font, 12))], 
            [sg.FileBrowse(
                button_text='Browse', 
                file_types=(('ONLY CSV FILES', '.csv'),), 
                initial_folder=None, 
                font=(font, 12),
                key='db'
            )],
            [sg.Text('Enter query', font=(font, 12))],
            [sg.Multiline(size=(50, 10), key='query', text_color='red')],
            [sg.Submit(button_text='Submit', font=(font, 12))]
        ]

# Create the Window
window = sg.Window('CX4031 Project 2 GUI', layout, element_justification='c').Finalize()
window.Maximize()
# Event Loop to process "events" and get the "values" of the inputs
while True:
    event, values = window.read()
    print('Database selected:', values['db'])
    print('You entered:', values['query'])

    if event == sg.WIN_CLOSED: # if user closes window
        break

window.close()