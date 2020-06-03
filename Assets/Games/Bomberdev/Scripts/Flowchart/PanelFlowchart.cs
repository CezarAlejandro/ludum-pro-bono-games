﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PanelFlowchart : MonoBehaviour {
    private List<GameObject> flowcharts = new List<GameObject>();
    private List<List<GameObject>> gridInstructions = new List<List<GameObject>>();
    private int indexFlowchart = 0;
    private int indexInstruction = 0;

    private GameObject instructionGameObjectSelected { get { return gridInstructions[indexFlowchart][indexInstruction]; } }
    private Instruction instructionSelected { get { return instructionGameObjectSelected.GetComponent<Instruction>(); } }

    private void Start() {
        flowcharts = new List<GameObject>();
        gridInstructions = new List<List<GameObject>>();
        foreach (Transform child in transform) {
            GameObject flowchart = child.gameObject;
            List<GameObject> instructions = flowchart.GetComponent<Flowchart>().instructions;
            flowcharts.Add(flowchart);
            gridInstructions.Add(instructions);
        }

        instructionSelected.Select();
    }

    private void Update() {
        Inputs();
    }

    private void MoveOrSelect(Direction direction) {
        if (instructionSelected.hold) Move(direction);
        else MoveSelection(direction);        
    }

    private void Inputs() {
        if (Input.GetKeyDown(Keys.up)) MoveOrSelect(Direction.UP);
        if (Input.GetKeyDown(Keys.down)) MoveOrSelect(Direction.DOWN);
        if (Input.GetKeyDown(Keys.left)) MoveOrSelect(Direction.LEFT);
        if (Input.GetKeyDown(Keys.right)) MoveOrSelect(Direction.RIGHT);
        if (Input.GetKeyDown(Keys.action1)) instructionSelected.ToggleHold();
    }

    private void MoveSelection(Direction direction) {
        int indexTranslationFlowchart = 0;
        int indexTranslationInstruction = 0;

        switch(direction) {
            case Direction.UP:
                indexTranslationInstruction = -1;
                break;
            case Direction.DOWN:
                indexTranslationInstruction = 1;
                break;
            case Direction.LEFT:
                indexTranslationFlowchart = -1;
                break;
            case Direction.RIGHT:
                indexTranslationFlowchart = 1;
                break;
        }

        instructionSelected.Unselect();

        indexFlowchart += indexTranslationFlowchart;
        indexInstruction += indexTranslationInstruction;

        if (indexFlowchart < 0) indexFlowchart = flowcharts.Count - 1;
        else if (indexFlowchart >= flowcharts.Count) indexFlowchart = 0;

        if (indexInstruction < 0) indexInstruction = gridInstructions[indexFlowchart].Count - 1;
        else if (indexInstruction >= gridInstructions[indexFlowchart].Count) indexInstruction = 0;

        instructionSelected.Select();
    }

    private void Move(Direction direction) {
        int indexTranslationFlowchart = 0;
        int indexTranslationInstruction = 0;

        switch(direction) {
            case Direction.UP:
                indexTranslationInstruction = -1;
                break;
            case Direction.DOWN:
                indexTranslationInstruction = 1;
                break;
            case Direction.LEFT:
                indexTranslationFlowchart = -1;
                break;
            case Direction.RIGHT:
                indexTranslationFlowchart = 1;
                break;
        }

        instructionSelected.Unselect();

        int newIndexFlowchart = indexFlowchart + indexTranslationFlowchart;
        int newIndexInstruction = indexInstruction + indexTranslationInstruction;

        if (newIndexFlowchart < 0) newIndexFlowchart = flowcharts.Count - 1;
        else if (newIndexFlowchart >= flowcharts.Count) newIndexFlowchart = 0;
        if (newIndexInstruction < 0) newIndexInstruction = gridInstructions[indexFlowchart].Count - 1;
        else if (newIndexInstruction >= gridInstructions[indexFlowchart].Count) newIndexInstruction = 0;

        GameObject instruction = Instantiate(instructionGameObjectSelected, flowcharts[newIndexFlowchart].transform);
        instruction.transform.SetSiblingIndex(newIndexInstruction + 1);
        Destroy(instructionGameObjectSelected);
        
        gridInstructions[indexFlowchart].RemoveAt(newIndexInstruction);
        gridInstructions[newIndexFlowchart].Insert(newIndexInstruction, instruction);

        indexFlowchart = newIndexFlowchart;
        indexInstruction = newIndexInstruction;
        
        instructionSelected.Hold();
    }
}
